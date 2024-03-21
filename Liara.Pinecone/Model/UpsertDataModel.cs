

using Newtonsoft.Json;

namespace Liara.Pinecone.Model;

public class UpsertDataModel
{
    [JsonProperty("vectors")]
    public IList<Vector> Vectors { get; set; } = default!;

    [JsonProperty("namespace")]
    public string Namespace { get; set; } = default!;
}
