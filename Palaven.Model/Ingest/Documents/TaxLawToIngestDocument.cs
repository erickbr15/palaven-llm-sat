using Newtonsoft.Json;

namespace Palaven.Model.Ingest.Documents;

public class TaxLawToIngestDocument
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = default!;

    [JsonProperty(PropertyName = "tenantId")]
    public string TenantId { get; set; } = default!;
    public Guid TraceId { get; set; }
    public Guid LawId { get; set; }    
    public string OriginalFileName { get; set; } = default!;
    public string FileName { get; set; } = default!;    
    public string AcronymLaw { get; set; } = default!;
    public string NameLaw { get; set; } = default!;
    public int YearLaw { get; set; }    
    public string LawDocumentVersion { get; set; } = default!;    
    public string DocumentType { get; set; } = default!;
}
