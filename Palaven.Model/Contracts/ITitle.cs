namespace Palaven.Model.Contracts;

public interface ITitle : IAdditionalInformation
{
    public Guid TitleId { get; set; }
    public Guid LawId { get; set; }
    string Name { get; set; }
    string? Topic { get; set; }
}
