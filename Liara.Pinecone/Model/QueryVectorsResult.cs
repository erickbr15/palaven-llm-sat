using Newtonsoft.Json;

namespace Liara.Pinecone.Model;

public class QueryVectorsResult
{
    [JsonProperty("namespace")]
    public string Namespace { get; set; } = default!;

    [JsonProperty("matches")]
    public IList<Match> Matches { get; set; } = default!;

    [JsonProperty("usage")]
    public Usage Usage { get; set; } = default!;
}
