namespace Palaven.Model.Ingest.Documents;

public class TaxLawArticleMetadata
{
    public Guid LawId { get; set; }
    public Guid ArticleId { get; set; }
    public IList<string> LlmFunctions { get; set; } = new List<string>();
}
