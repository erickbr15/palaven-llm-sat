namespace Palaven.Model.Contracts;

public interface IFraction : IAdditionalInformation
{
    Guid FractionId { get; set; }    
    Guid LawId { get; set; }
    Guid TitleId { get; set; }
    Guid ChapterId { get; set; }
    Guid SectionId { get; set; }
    Guid ArticleId { get; set; }
    Guid ParagraphId { get; set; }
    string ConsecutiveRoman { get; set; }    
    string? Topic { get; set; }
    string Text { get; set; }
}
