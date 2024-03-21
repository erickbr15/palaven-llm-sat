using Liara.Common;
using Liara.CosmosDb;
using Liara.OpenAI;
using Liara.OpenAI.Model.Chat;
using Liara.OpenAI.Model.Embeddings;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Palaven.Model.Ingest.Commands;
using Palaven.Model.Ingest.Documents;
using System.Net;

namespace Palaven.Ingest.Commands;

public class CreateTaxLawGoldenArticleDocument : ITraceableCommand<CreateGoldenArticleDocumentModel, Guid>
{
    private readonly IOpenAiServiceClient _openAiChatService;
    private readonly IDocumentRepository<TaxLawDocumentArticle> _articleDocumentRepository;
    private readonly IDocumentRepository<TaxLawDocumentGoldenArticle> _goldenArticleDocumentRepository;

    public CreateTaxLawGoldenArticleDocument(IOpenAiServiceClient openAiChatService,
        IDocumentRepository<TaxLawDocumentArticle> articleDocumentRepository,
        IDocumentRepository<TaxLawDocumentGoldenArticle> goldenArticleDocumentRepository)
    {
        _openAiChatService = openAiChatService ?? throw new ArgumentNullException(nameof(openAiChatService));
        _articleDocumentRepository = articleDocumentRepository ?? throw new ArgumentNullException(nameof(articleDocumentRepository));
        _goldenArticleDocumentRepository = goldenArticleDocumentRepository ?? throw new ArgumentNullException(nameof(goldenArticleDocumentRepository));
    }

    public async Task<IResult<Guid>> ExecuteAsync(Guid traceId, CreateGoldenArticleDocumentModel inputModel, CancellationToken cancellationToken)
    {
        var tenantId = new Guid("69A03A54-4181-4D50-8274-D2D88EA911E4");

        var query = new QueryDefinition($"SELECT * FROM c WHERE c.id = \"{inputModel.ArticleId}\"");

        var queryResults = await _articleDocumentRepository.GetAsync(query, continuationToken: null,
            new QueryRequestOptions { PartitionKey = new PartitionKey(tenantId.ToString()) },
            cancellationToken);

        var article = queryResults.SingleOrDefault() ?? throw new InvalidOperationException($"Unable to find the tax law document article with id {inputModel.ArticleId}");

        var goldenArticleId = Guid.NewGuid();

        var goldenArticle = new TaxLawDocumentGoldenArticle
        {
            Id = goldenArticleId.ToString(),
            TenantId = tenantId.ToString(),
            TraceId = traceId,
            LawId = article.LawId,
            ArticleId = inputModel.ArticleId,
            LawDocumentVersion = article.LawDocumentVersion,
            Article = article.Article,
            Content = article.Content,
            DocumentType = nameof(TaxLawDocumentGoldenArticle)
        };

        await PopulateGoldenArticleQuestionsAsync(goldenArticle, article.Content, cancellationToken);
        
        await PopulateGoldenArticleShortSummaryAsync(goldenArticle, article.Content, cancellationToken);

        var result = await _goldenArticleDocumentRepository.CreateAsync(goldenArticle, new PartitionKey(tenantId.ToString()), itemRequestOptions: null, cancellationToken);

        if (result.StatusCode != HttpStatusCode.Created)
        {
            throw new InvalidOperationException($"Unable to create the account file document. Status code: {result.StatusCode}");
        }

        return Result<Guid>.Success(goldenArticleId);
    }

    private async Task PopulateGoldenArticleQuestionsAsync(TaxLawDocumentGoldenArticle goldenArticle, string articleContent, CancellationToken cancellationToken)
    {
        var chatGptQuestions = await CreateQuestionsAsync(articleContent, cancellationToken);
        if (chatGptQuestions == null || !chatGptQuestions.Success)
        {
            throw new InvalidOperationException($"Unable to create questions from the article with id {goldenArticle.ArticleId}");
        }

        foreach (var question in chatGptQuestions.Questions)
        {
            var embeddings = await ComputeEmbeddingsAsync(new List<string> { question }, cancellationToken);
            if (embeddings.Count != 1)
            {
                throw new InvalidOperationException($"Unable to compute embeddings for the questions from the article with id {goldenArticle.ArticleId}");
            }

            goldenArticle.Questions.Add(new TaxLawArticleQuestion
            {
                Question = question,
                Embedding = embeddings[0].EmbeddingVector.Select(e => (double)e).ToList(),
                Metadata = new TaxLawArticleMetadata
                {
                    LawId = goldenArticle.LawId,
                    ArticleId = goldenArticle.ArticleId,
                    LlmFunctions = new List<string> { "question-answering" }
                }
            });
        }
    }

    private async Task PopulateGoldenArticleShortSummaryAsync(TaxLawDocumentGoldenArticle goldenArticle, string articleContent, CancellationToken cancellationToken)
    {
        var chatGptShortSummary = await CreateShortSummaryAsync(articleContent, cancellationToken);
        if (chatGptShortSummary == null || !chatGptShortSummary.Success)
        {
            throw new InvalidOperationException($"Unable to create short summary from the article with id {goldenArticle.ArticleId}");
        }

        var embeddings = await ComputeEmbeddingsAsync(new List<string> { chatGptShortSummary.Summary }, cancellationToken);
        if (embeddings.Count != 1)
        {
            throw new InvalidOperationException($"Unable to compute embeddings for the short summary from the article with id {goldenArticle.ArticleId}");
        }

        goldenArticle.ShortSummary = new TaxLawShortSummary
        {
            ShortSummary = chatGptShortSummary.Summary,
            Embedding = embeddings[0].EmbeddingVector.Select(e => (double)e).ToList(),
            Metadata = new TaxLawArticleMetadata
            {
                LawId = goldenArticle.LawId,
                ArticleId = goldenArticle.ArticleId,
                LlmFunctions = new List<string> { "summarization" }
            }
        };
    }

    private async Task<QuestionsFromArticleChatGptResponse?> CreateQuestionsAsync(string articleContent, CancellationToken cancellationToken)
    {
        var chatGptCallOptions = new ChatCompletionCreationModel
        {
            Model = "gpt-3.5-turbo",
            Temperature = 0.6m,
            User = new Guid("69A03A54-4181-4D50-8274-D2D88EA911E4").ToString(),
            ResponseFormat = new ResponseFormat { Type = "json_object" }
        };

        var messages = new List<Message>
        {
            new Message
            {
                Role = "system",
                Content = "You are a very capable AI assistant that will create a set of intelligent and interesting questions about a given text in spanish language."
            },
            new Message
            {
                Role = "user",
                Content = Resources.ChatGptPromptTemplates.CreateQuestionsFromArticlePromptTemplate.Replace("{working_text}", articleContent)
            }
        };

        var chatGptResponse = await _openAiChatService.CreateChatCompletionAsync(messages, chatGptCallOptions, cancellationToken);
        var completionResult = JsonConvert.DeserializeObject<QuestionsFromArticleChatGptResponse>(
                chatGptResponse!.Choices.FirstOrDefault()?.Message.Content ?? string.Empty);

        return completionResult;
    }

    private async Task<ArticleShortSummaryChatGptResponse?> CreateShortSummaryAsync(string articleContent, CancellationToken cancellationToken)
    {
        var chatGptCallOptions = new ChatCompletionCreationModel
        {
            Model = "gpt-3.5-turbo",
            Temperature = 0.0m,
            User = new Guid("69A03A54-4181-4D50-8274-D2D88EA911E4").ToString(),
            ResponseFormat = new ResponseFormat { Type = "json_object" }
        };

        var messages = new List<Message>
        {
            new Message
            {
                Role = "system",
                Content = "You are a very capable AI assistant that will create a short summary of the given text in spanish language."
            },
            new Message
            {
                Role = "user",
                Content = Resources.ChatGptPromptTemplates.CreateShortSummaryPromptTemplate.Replace("{working_text}", articleContent)
            }
        };

        var chatGptResponse = await _openAiChatService.CreateChatCompletionAsync(messages, chatGptCallOptions, cancellationToken);
        var completionResult = JsonConvert.DeserializeObject<ArticleShortSummaryChatGptResponse>(
                           chatGptResponse!.Choices.FirstOrDefault()?.Message.Content ?? string.Empty);

        return completionResult;
    }

    private async Task<IList<Embedding>> ComputeEmbeddingsAsync(IList<string> input, CancellationToken cancellationToken)
    {
        var tenantId = new Guid("69A03A54-4181-4D50-8274-D2D88EA911E4");
        var inputModel = new CreateEmbeddingsModel
        {
            User = tenantId.ToString(),
            Input = input
        };

        var chatGptResponse = await _openAiChatService.CreateEmbeddingsAsync(inputModel, cancellationToken);
        
        return chatGptResponse?.Data?? new List<Embedding>();
    }
}
