namespace Palaven.Model.Contracts;

public interface IArticle : IAdditionalInformation
{
    Guid ArticleId { get; set; }    
    Guid LawId { get; set; }
    Guid TitleId { get; set; }
    Guid ChapterId { get; set; }
    Guid SectionId { get; set; }
    string Name { get; set; }
    string? Topic { get; set; }
}
