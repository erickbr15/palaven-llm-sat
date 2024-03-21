using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Embeddings;

public class CreateEmbeddingsBody
{
    [JsonProperty("input")]
    public object Input { get; set; } = default!;

    [JsonProperty("model")]
    public string Model { get; set; } = default!;

    [JsonProperty("encoding_format")]
    public string EncodingFormat { get; set; } = default!;

    [JsonProperty("user")]
    public string? User { get; set; } = default!;
}
