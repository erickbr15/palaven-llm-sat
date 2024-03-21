namespace Palaven.Model.Ingest.Documents;

public class TaxLawShortSummary
{
    public string ShortSummary { get; set; } = default!;
    public IList<double> Embedding { get; set; } = default!;
    public TaxLawArticleMetadata Metadata { get; set; } = default!;
}
