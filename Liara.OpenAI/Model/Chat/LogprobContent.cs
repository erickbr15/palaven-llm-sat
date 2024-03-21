using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class LogprobContent
{
    [JsonProperty("token")]
    public string Token { get; set; } = default!;

    [JsonProperty("logprob")]
    public int Logprob { get; set; } = default!;

    [JsonProperty("bytes")]
    public int[] Bytes { get; set; } = default!;

    [JsonProperty("top_logprobs")]
    public IList<TopLogprobContent>? TopLogprobs { get; set; }
}
