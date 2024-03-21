using Newtonsoft.Json;

namespace Liara.OpenAI.Model;

public class Usage
{
    [JsonProperty("completion_tokens")]
    public int CompletionTokens { get; set; } = default!;

    [JsonProperty("prompt_tokens")]
    public int PromptTokens { get; set; } = default!;

    [JsonProperty("total_tokens")]
    public int TotalTokens { get; set; } = default!;
}
