namespace Palaven.Model.Contracts;

public interface ISubsection : IAdditionalInformation
{
    Guid SubsectionId { get; set; }    
    Guid LawId { get; set; }
    Guid TitleId { get; set; }
    Guid ChapterId { get; set; }
    Guid SectionId { get; set; }
    Guid ArticleId { get; set; }
    Guid ParagraphId { get; set; }
    Guid FractionId { get; set; }
    string Letter { get; set; }
    string? Topic { get; set; }
    string Text { get; set; }
}
