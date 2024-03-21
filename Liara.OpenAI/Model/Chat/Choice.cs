using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class Choice
{
    [JsonProperty("finish_reason")]
    public string FinishReason { get; set; } = default!;

    [JsonProperty("index")]
    public int Index { get; set; } = default!;

    [JsonProperty("message")]
    public MessageResponse Message { get; set; } = default!;

    [JsonProperty("logprobs")]
    public Logprob Logprob { get; set; } = default!;

}
