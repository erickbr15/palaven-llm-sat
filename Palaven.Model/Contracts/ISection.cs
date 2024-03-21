namespace Palaven.Model.Contracts;

public interface ISection : IAdditionalInformation
{
    Guid SectionId { get; set; }    
    Guid LawId { get; set; }
    Guid TitleId { get; set; }
    Guid ChapterId { get; set; }
    string Name { get; set; }
    string? Topic { get; set; }
}
