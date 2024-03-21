namespace Palaven.Model.Contracts;

public interface ITextIdentifier
{        
    Guid LawId { get; set; }
    Guid TitleId { get; set; }
    Guid ChapterId { get; set; }
    Guid SectionId { get; set; }
    Guid ArticleId { get; set; }
    Guid ParagraphId { get; set; }
    Guid FractionId { get; set; }
    Guid SubsectionId { get; set; }
    Guid NumberId { get; set; }
}
