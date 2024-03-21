using Newtonsoft.Json;

namespace Liara.Pinecone.Model;

public class ReadUnits
{
    [JsonProperty("read_units")]
    public long ReadUnitsValue { get; set; }
}
