namespace Palaven.Model.Contracts;

public interface ICorrelation
{
    ITextIdentifier Source { get; set; }
    ITextIdentifier Target { get; set; }
}
