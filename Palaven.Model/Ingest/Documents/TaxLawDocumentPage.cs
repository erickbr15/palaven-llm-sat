using Azure.AI.FormRecognizer.DocumentAnalysis;
using Newtonsoft.Json;

namespace Palaven.Model.Ingest.Documents;

public class TaxLawDocumentPage
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = default!;

    [JsonProperty(PropertyName = "tenantId")]
    public string TenantId { get; set; } = default!;
    public Guid TraceId { get; set; }
    public Guid LawId { get; set; }
    public string LawDocumentVersion { get; set; } = default!;
    public int PageNumber { get; set; }
    public IList<TaxLawDocumentLine> Lines { get; set; } = new List<TaxLawDocumentLine>();
    public string DocumentType { get; set; } = default!;
}
