using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class Logprob
{
    [JsonProperty("content")]
    public IList<LogprobContent>? Content { get; set; }
}
