using Newtonsoft.Json;

namespace Palaven.Model.Ingest.Commands;

public class QuestionsFromArticleChatGptResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("questions")]
    public IList<string> Questions { get; set; } = new List<string>();
}
