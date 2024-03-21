using Newtonsoft.Json;

namespace Liara.OpenAI.Model.Chat;

public class Function
{
    [JsonProperty("description")]
    public string Description { get; set; } = default!;

    [JsonProperty("name")]
    public string Name { get; set; } = default!;

    [JsonProperty("parameters")]
    public object Parameters { get; set; } = default!;
}
