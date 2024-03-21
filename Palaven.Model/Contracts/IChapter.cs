namespace Palaven.Model.Contracts;

public interface IChapter : IAdditionalInformation
{
    Guid ChapterId { get; set; }
    Guid LawId { get; set; }
    Guid TitleId { get; set; }
    string Name { get; set; }
    string? Topic { get; set; }
}
