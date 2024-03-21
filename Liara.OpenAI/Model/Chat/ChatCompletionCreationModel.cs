namespace Liara.OpenAI.Model.Chat;

public class ChatCompletionCreationModel
{
    public string Model { get; set; } = default!;
    public int? FrequencyPenalty { get; set; }
    public bool? LogProbs { get; set; }
    public int? TopLogProbs { get; set; }
    public int? MaxTokens { get; set; }
    public int? N { get; set; }
    public int? PresencePenalty { get; set; }
    public ResponseFormat? ResponseFormat { get; set; } = default!;
    public int? Seed { get; set; }
    public bool? Stream { get; set; }
    public decimal? Temperature { get; set; }
    public int? TopP { get; set; }
    public IList<Tool>? Tools { get; set; }
    public object? ToolChoice { get; set; }
    public string? User { get; set; }
}
