using Palaven.Model.Contracts;

namespace Palaven.Model.Domain;

public class Paragraph : IParagraph
{
    public Guid ParagraphId { get; set; }
    public Guid LawId { get; set; }
    public Guid TitleId { get; set; }
    public Guid ChapterId { get; set; }
    public Guid SectionId { get; set; }
    public Guid ArticleId { get; set; }
    public int ParagraphNumber { get; set; }
    public string? Topic { get; set; }
    public string Text { get; set; } = default!;
    public IList<ICorrelation> Correlations { get; set; } = new List<ICorrelation>();
    public IList<string> AdditionalInformation { get; set; } = new List<string>();
}
