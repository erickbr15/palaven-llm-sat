using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public sealed class MessageResponse : Message
{
    [JsonProperty("tool_calls")]
    public IList<ToolCall> ToolCalls { get; set; } = new List<ToolCall>();
}
