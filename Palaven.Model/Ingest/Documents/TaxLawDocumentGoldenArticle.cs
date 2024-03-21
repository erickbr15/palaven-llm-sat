using Newtonsoft.Json;

namespace Palaven.Model.Ingest.Documents;

public class TaxLawDocumentGoldenArticle
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = default!;

    [JsonProperty(PropertyName = "tenantId")]
    public string TenantId { get; set; } = default!;
    public Guid TraceId { get; set; }
    public Guid LawId { get; set; }
    public Guid ArticleId { get; set; }
    public string LawDocumentVersion { get; set; } = default!;
    public string Article { get; set; } = default!;
    public string Content { get; set; } = default!;    
    public TaxLawShortSummary ShortSummary { get; set; } = default!;
    public IList<TaxLawArticleQuestion> Questions { get; set; } = new List<TaxLawArticleQuestion>();    
    public string DocumentType { get; set; } = default!;
}
