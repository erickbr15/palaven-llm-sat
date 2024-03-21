using Newtonsoft.Json;

namespace Palaven.Model.Ingest.Documents;

public class TaxLawDocumentArticle
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = default!;

    [JsonProperty(PropertyName = "tenantId")]
    public string TenantId { get; set; } = default!;
    public Guid TraceId { get; set; }
    public Guid LawId { get; set; }
    public string LawDocumentVersion { get; set; } = default!;
    public string Article { get; set; } = default!;
    public string Content { get; set; } = default!;
    public List<Guid> ArticleLineIds { get; set; } = new List<Guid>();
    public string DocumentType { get; set; } = default!;
}
