namespace Liara.OpenAI.Model.Embeddings;

public class CreateEmbeddingsModel
{
    public IList<string> Input { get; set; } = default!;
    public string User { get; set; } = default!;
}
