using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class ToolChoiceFunction
{
    [JsonProperty("name")]
    public string Name { get; set; } = default!;
}
