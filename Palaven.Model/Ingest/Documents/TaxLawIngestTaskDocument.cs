namespace Palaven.Model.Ingest.Documents;

public class TaxLawIngestTaskDocument
{
    public Guid id { get; set; }
    public Guid TaskId { get; set; }
    public string TaskType { get; set; } = default!;
    public object TaskData { get; set; } = default!;
}
