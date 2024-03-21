using Liara.OpenAI.Model.Chat;
using Liara.OpenAI.Model.Embeddings;

namespace Liara.OpenAI;

public interface IOpenAiServiceClient
{
    Task<ChatCompletionResponse?> CreateChatCompletionAsync(IEnumerable<Message> messages, ChatCompletionCreationModel inputModel, CancellationToken cancellationToken);
    Task<CreateEmbeddingResponse?> CreateEmbeddingsAsync(CreateEmbeddingsModel inputModel, CancellationToken cancellationToken);
}
