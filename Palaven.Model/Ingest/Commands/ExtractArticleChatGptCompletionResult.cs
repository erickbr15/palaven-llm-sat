using Newtonsoft.Json;

namespace Palaven.Model.Ingest.Commands;

public class ExtractArticleChatGptCompletionResult
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("article")]
    public string Article { get; set; } = default!;

    [JsonProperty("content")]
    public string Content { get; set; } = default!;

    [JsonProperty("references")]
    public IList<string> References { get; set; } = new List<string>();
}
