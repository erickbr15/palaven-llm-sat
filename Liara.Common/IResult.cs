namespace Liara.Common;

public interface IResult
{
    List<Exception> Errors { get; }
    List<ValidationError> ValidationErrors { get; }
    bool AnyErrors { get; }
    bool AnyValidationErrors { get; }
    bool AnyErrorsOrValidationFailures => Errors.Any() || ValidationErrors.Any();
}
