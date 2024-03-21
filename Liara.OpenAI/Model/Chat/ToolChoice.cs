using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class ToolChoice
{
    [JsonProperty("type")]
    public string Type { get; set; } = default!;

    [JsonProperty("function")]
    public ToolChoiceFunction Function { get; set; } = default!;
}
