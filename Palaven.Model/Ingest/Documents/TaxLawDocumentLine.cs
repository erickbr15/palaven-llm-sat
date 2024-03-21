namespace Palaven.Model.Ingest.Documents;

public sealed class TaxLawDocumentLine
{
    public Guid PageDocumentId { get; set; }
    public int PageNumber { get; set; }
    public int LineNumber { get; set; }
    public Guid LineId { get; set; }
    public string Content { get; set; } = default!;
    public List<System.Drawing.PointF> BoundingBox { get; set; } = new List<System.Drawing.PointF>();
}
