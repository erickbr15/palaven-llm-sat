using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Liara.OpenAI.Model.Embeddings;

public class Embedding
{
    [JsonProperty("index")]
    public int Index { get; set; }

    [JsonProperty("embedding")]
    public JArray EmbeddingVector { get; set; } = default!;

    [JsonProperty("object")]
    public string Object { get; set; } = default!;
}
