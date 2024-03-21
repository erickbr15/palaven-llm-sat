namespace Liara.Common;

public interface IResult<TResult> : IResult
{
    TResult Value { get; set; }
}
