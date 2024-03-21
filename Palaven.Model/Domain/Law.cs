using Palaven.Model.Contracts;

namespace Palaven.Model.Domain;

public class Law : ILaw
{
    public Guid LawId { get; set; }
    public string Acronym { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Year { get; set; }
    public IList<ICorrelation> Correlations { get; set; } = new List<ICorrelation>();
    public IList<string> AdditionalInformation { get; set; } = new List<string>();    
}
