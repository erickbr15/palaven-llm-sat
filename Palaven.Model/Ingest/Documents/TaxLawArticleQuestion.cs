namespace Palaven.Model.Ingest.Documents;

public class TaxLawArticleQuestion
{
    public string Question { get; set; } = default!;
    public IList<double> Embedding { get; set; } = default!;
    public TaxLawArticleMetadata Metadata { get; set; } = default!;
}
