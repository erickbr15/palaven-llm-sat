namespace Palaven.Model.Contracts
{
    public interface IAdditionalInformation
    {
        IList<ICorrelation> Correlations { get; set; }
        IList<string> AdditionalInformation { get; set; }
    }
}
