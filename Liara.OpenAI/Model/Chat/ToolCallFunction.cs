using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class ToolCallFunction
{
    [JsonProperty("name")]
    public string Name { get; set; } = default!;

    [JsonProperty("arguments")]
    public string Arguments { get; set; } = default!;
}
