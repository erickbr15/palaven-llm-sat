using Newtonsoft.Json;

namespace Liara.Pinecone.Model;

public class QueryVectorsModel
{
    [JsonProperty("namespace")]
    public string? Namespace { get; set; }

    [JsonProperty("topK")]
    public Int64 TopK { get; set; }

    [JsonProperty("vector")]
    public IList<double> Vector { get; set; } = default!;

    [JsonProperty("includeValues")]
    public bool IncludeValues { get; set; }

    [JsonProperty("includeMetadata")]
    public bool IncludeMetadata { get; set; }
}
