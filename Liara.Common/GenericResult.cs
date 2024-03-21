namespace Liara.Common;

public sealed class Result<TResult> : IResult<TResult>
{
    public List<Exception> Errors { get; private set; } = new List<Exception>();
    public List<ValidationError> ValidationErrors { get; private set; } = new List<ValidationError>();
    public bool AnyErrors => Errors.Any();
    public bool AnyValidationErrors => ValidationErrors.Any();
    public bool IsValid => !Errors.Any() && !ValidationErrors.Any();
    public TResult Value { get; set; } = default!;
    
    public static IResult<TResult> Fail(IEnumerable<ValidationError> validationErrors, IEnumerable<Exception> errors)
    {
        var result = new Result<TResult>();
        
        result.Errors.AddRange(errors ?? new List<Exception>());
        result.ValidationErrors.AddRange(validationErrors ?? new List<ValidationError>());
        
        return result;
    }

    public static IResult<TResult> Success(TResult value)
    {
        var result = new Result<TResult>
        {
            Value = value
        };
        return result;
    }
}
