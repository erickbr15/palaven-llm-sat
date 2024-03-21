namespace Palaven.Model.Contracts;

public interface IParagraph : IAdditionalInformation
{
    Guid ParagraphId { get; set; }    
    Guid LawId { get; set; }
    Guid TitleId { get; set; }
    Guid ChapterId { get; set; }
    Guid SectionId { get; set; }
    Guid ArticleId { get; set; }
    int ParagraphNumber { get; set; }
    string? Topic { get; set; }
    string Text { get; set; }
}
