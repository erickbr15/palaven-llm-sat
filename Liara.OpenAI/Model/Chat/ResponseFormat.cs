using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class ResponseFormat
{
    [JsonProperty("type")]
    public string Type { get; set; } = default!;
}
