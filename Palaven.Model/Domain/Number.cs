using Palaven.Model.Contracts;

namespace Palaven.Model.Domain;

public class Number : INumber
{
    public Guid NumberId { get; set; }
    public Guid LawId { get; set; }
    public Guid TitleId { get; set; }
    public Guid ChapterId { get; set; }
    public Guid SectionId { get; set; }
    public Guid ArticleId { get; set; }
    public Guid ParagraphId { get; set; }
    public Guid FractionId { get; set; }
    public Guid SubsectionId { get; set; }
    public int ConsecutiveNumber { get; set; }
    public string? Topic { get; set; }
    public string Text { get; set; } = default!;
    public IList<ICorrelation> Correlations { get; set; } = new List<ICorrelation>();
    public IList<string> AdditionalInformation { get; set; } = new List<string>();
}
