using Liara.Common.Http;
using Liara.OpenAI.Model.Chat;
using Liara.OpenAI.Model.Embeddings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Liara.OpenAI;

public class OpenAiServiceClient : IOpenAiServiceClient
{
    private readonly IHttpProxy _httpProxy;
    private readonly OpenAiOptions _openAiOptions;

    public OpenAiServiceClient(IOptions<OpenAiOptions> options, IHttpProxy httpProxy)
    {
        _openAiOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _httpProxy = httpProxy ?? throw new ArgumentNullException(nameof(httpProxy));
    }

    public async Task<ChatCompletionResponse?> CreateChatCompletionAsync(IEnumerable<Message> messages, ChatCompletionCreationModel inputModel, CancellationToken cancellationToken)
    {
        var headers = new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {_openAiOptions.ApiKey}" }
        };

        var chatCompletionBody = new ChatCompletionBodyBuilder().NewWith(messages, inputModel).Build();
        string content = JsonConvert.SerializeObject(chatCompletionBody);
        var buffer = Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpProxy.PostAsync(new Uri(_openAiOptions.ChatEndpointUrl),
            headers,
            byteContent,
            cancellationToken);

        var completionResponse = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>(cancellationToken: cancellationToken);

        return completionResponse;
    }

    public async Task<CreateEmbeddingResponse?> CreateEmbeddingsAsync(CreateEmbeddingsModel inputModel, CancellationToken cancellationToken)
    {
        var headers = new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {_openAiOptions.ApiKey}" }
        };

        var requestBody = new CreateEmbeddingsBodyBuilder().NewWithDefaults(inputModel.User, inputModel.Input).Build();
        string content = JsonConvert.SerializeObject(requestBody);
        var buffer = Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


        var response = await _httpProxy.PostAsync(new Uri(_openAiOptions.EmbeddingsEndpointUrl),
            headers,
            byteContent,
            cancellationToken);

        var embeddings = await response.Content.ReadFromJsonAsync<dynamic>(cancellationToken: cancellationToken);
        var embeddingsArrayText = embeddings?.GetProperty("data").ToString();
        
        var embeddingResponse = new CreateEmbeddingResponse
        {
            Data = JsonConvert.DeserializeObject<List<Embedding>>(embeddingsArrayText)
        };

        return embeddingResponse;
    }
}
