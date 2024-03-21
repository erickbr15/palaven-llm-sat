using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class Tool
{
    [JsonProperty("type")]
    public string Type { get; set; } = default!;

    [JsonProperty("function")]
    public Function Function { get; set; } = default!;
}
