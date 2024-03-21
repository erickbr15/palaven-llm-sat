namespace Palaven.Model.Ingest.Tasks;

public class StartIngestTask
{
    public Guid TaskId { get; set; }
    public string OriginalFileName { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string FileExtension { get; set; } = default!;
}
