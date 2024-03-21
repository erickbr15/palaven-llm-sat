namespace Palaven.Model.Contracts;

public interface ILaw : IAdditionalInformation
{
    Guid LawId { get; set; }
    string Acronym { get; set; }
    string Name { get; set; }
    int Year { get; set; }
}
