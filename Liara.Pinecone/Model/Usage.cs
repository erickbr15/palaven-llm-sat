using Newtonsoft.Json;

namespace Liara.Pinecone.Model;

public class Usage
{
    [JsonProperty("readUnits")]
    public dynamic ReadUnits { get; set; } = default!;
}
