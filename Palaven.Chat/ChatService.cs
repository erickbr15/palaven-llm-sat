using Liara.CosmosDb;
using Liara.OpenAI;
using Liara.OpenAI.Model.Chat;
using Liara.OpenAI.Model.Embeddings;
using Liara.Pinecone;
using Liara.Pinecone.Model;
using Microsoft.Azure.Cosmos;
using Palaven.Model.Chat;
using Palaven.Model.Ingest.Documents;

namespace Palaven.Chat;

public class ChatService : IChatService
{
    private readonly IPineconeServiceClient _pineconeServiceClient;
    private readonly IOpenAiServiceClient _openAiServiceClient;
    private readonly IDocumentRepository<TaxLawDocumentGoldenArticle> _articleRepository;

    public ChatService(IPineconeServiceClient pineconeServiceClient, IOpenAiServiceClient openAiServiceClient, IDocumentRepository<TaxLawDocumentGoldenArticle> articleRepository)
    {
        _pineconeServiceClient = pineconeServiceClient ?? throw new ArgumentNullException(nameof(pineconeServiceClient));
        _openAiServiceClient = openAiServiceClient ?? throw new ArgumentNullException(nameof(openAiServiceClient));
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
    }

    public async Task<string> GetChatResponseAsync(ChatMessage chatMessage, CancellationToken cancellationToken)
    {            
        var relatedArticles = await TryGetRelevantArticlesAsync(chatMessage, cancellationToken);

        var messages = BuildChatMessages(chatMessage.Query, relatedArticles);

        var completionModel = new ChatCompletionCreationModel
        {
            Model = "gpt-4",
            User = chatMessage.UserId,
            Temperature = 0.65m
        };

        var chatCompletion = await _openAiServiceClient.CreateChatCompletionAsync(messages, completionModel, cancellationToken);

        var chatResponse = chatCompletion!.Choices.Any() ? chatCompletion.Choices[0].Message.Content : string.Empty;

        return chatResponse;
    }

    private IEnumerable<Message> BuildChatMessages(string query, IEnumerable<TaxLawDocumentGoldenArticle> relatedArticles)
    {
        const string userQueryMark = "{user_query}";
        const string userAdditionalInfoMark = "{additional_info}";

        var additionalInformation = relatedArticles.Any() ? $"<additional_info>{string.Join("\n\n\n", relatedArticles.Select(r=>r.Content))}</additional_info>" : string.Empty;

        var userMessageContent = Resources.ChatGptPromptTemplates.AnswerMexicanTaxLawQuestionUserRole
            .Replace(userQueryMark, query)
            .Replace(userAdditionalInfoMark, additionalInformation);

        var messages = new List<Message>
        {
            new Message
            {
                Role = "system",
                Content = Resources.ChatGptPromptTemplates.AnswerMexicanTaxLawQuestionSystemRole
            },
            new Message
            {
                Role = "user",
                Content = userMessageContent
            },
        };

        return messages;
    }

    private async Task<IEnumerable<TaxLawDocumentGoldenArticle>> TryGetRelevantArticlesAsync(ChatMessage message, CancellationToken cancellationToken)
    {
        var createQueryEmbeddingsRequest = new CreateEmbeddingsModel
        {
            User = message.UserId,
            Input = new List<string> { message.Query }
        };

        var queryEmbeddings = await _openAiServiceClient.CreateEmbeddingsAsync(createQueryEmbeddingsRequest, cancellationToken);
        if(queryEmbeddings == null || queryEmbeddings.Data == null || !queryEmbeddings.Data.Any())
        {
            return new List<TaxLawDocumentGoldenArticle>();
        }

        var queryVectorsModel = new QueryVectorsModel
        {
            IncludeMetadata = true,
            TopK = 3,
            Namespace = "LISR-2024",
            Vector = queryEmbeddings.Data[0].EmbeddingVector.Select(v => (double)v).ToList()
        };

        var queryVectorResult = await _pineconeServiceClient.QueryVectorsAsync(queryVectorsModel, cancellationToken);
        if(queryVectorResult == null || queryVectorResult.Matches == null || !queryVectorResult.Matches.Any(match=>match.Score >= 0.8))
        {
            return new List<TaxLawDocumentGoldenArticle>();
        }

        var articleIds = queryVectorResult.Matches
            .SelectMany(m=>m.Metadata)
            .Where(m=>m.Key == "articleId")
            .Select(m=>m.Value.ToString())
            .ToList();

        var goldenArticlesQuery = $"SELECT * FROM c WHERE c.id IN ({string.Join(",", articleIds.Select(a => $"'{a}'"))})";

        var tenantId = new Guid("69A03A54-4181-4D50-8274-D2D88EA911E4");

        var goldenArticles = await _articleRepository.GetAsync(new QueryDefinition(goldenArticlesQuery),
            continuationToken: null,
            new QueryRequestOptions { PartitionKey = new PartitionKey(tenantId.ToString()) },
            cancellationToken: cancellationToken);

        return goldenArticles.ToList();
    }
}
