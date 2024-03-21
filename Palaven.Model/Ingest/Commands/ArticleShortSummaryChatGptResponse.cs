using Newtonsoft.Json;

namespace Palaven.Model.Ingest.Commands;

public class ArticleShortSummaryChatGptResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("summary")]
    public string Summary { get; set; } = default!;
}
