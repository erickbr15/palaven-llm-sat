namespace Palaven.Model.Ingest.Commands;

public class IngestLawDocumentModel
{
    public string Acronym { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Year { get; set; }
    public string FileName { get; set; } = default!;
    public string FileExtension { get; set; } = default!;
    public byte[] FileContent { get; set; } = default!;
    public string LawDocumentVersion { get; set; } = default!;
}
