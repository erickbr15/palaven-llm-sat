using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class ChatCompletionResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = default!;

    [JsonProperty("choices")]
    public IList<Choice> Choices { get; set; } = default!;

    [JsonProperty("created")]
    public int Created { get; set; } = default!;

    [JsonProperty("model")]
    public string Model { get; set; } = default!;

    [JsonProperty("system_fingerprint")]
    public string SystemFingerprint { get; set; } = default!;

    [JsonProperty("object")]
    public string Object { get; set; } = default!;

    [JsonProperty("usage")]
    public Usage Usage { get; set; } = default!;
}
