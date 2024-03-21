using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Embeddings;

public class CreateEmbeddingResponse
{
    [JsonProperty("object")]
    public string Object { get; set; } = default!;

    [JsonProperty("data")]
    public IList<Embedding> Data { get; set; } = default!;

    [JsonProperty("model")]
    public string Model { get; set; } = default!;

    [JsonProperty("usage")]
    public Usage Usage { get; set; } = default!;
}
