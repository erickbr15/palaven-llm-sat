using Newtonsoft.Json;

namespace Liara.Pinecone.Model;

public class Vector
{
    [JsonProperty("id")]
    public string Id { get; set; } = default!;

    [JsonProperty("values")]
    public IList<double> Values { get; set; } = default!;

    [JsonProperty("metadata")]
    public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
}
