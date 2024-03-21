using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class ChatCompletionBody
{
    [JsonProperty("messages")]
    public List<Message> Messages { get; set; } = new List<Message>();

    [JsonProperty("model")]
    public string Model { get; set; } = default!;

    [JsonProperty("frequency_penalty")]
    public int? FrequencyPenalty { get; set; }

    [JsonProperty("logprobs")]
    public bool? LogProbs { get; set; }

    [JsonProperty("top_logprobs")]
    public int? TopLogProbs { get; set; }

    [JsonProperty("max_tokens")]
    public int? MaxTokens { get; set; }

    [JsonProperty("n")]
    public int? N { get; set; }

    [JsonProperty("presence_penalty")]
    public int? PresencePenalty { get; set; }

    [JsonProperty("response_format")]
    public ResponseFormat ResponseFormat { get; set; } = default!;

    [JsonProperty("seed")]
    public int? Seed { get; set; }

    [JsonProperty("stream")]
    public bool? Stream { get; set; }

    [JsonProperty("temperature")]
    public decimal? Temperature { get; set; }

    [JsonProperty("top_p")]
    public int? TopP { get; set; }

    [JsonProperty("tools")]
    public IList<Tool>? Tools { get; set; }

    [JsonProperty("tool_choice")]
    public object? ToolChoice { get; set; }

    [JsonProperty("user")]
    public string? User { get; set; }
}
