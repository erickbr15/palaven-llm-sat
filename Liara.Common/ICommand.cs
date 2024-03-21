namespace Liara.Common;

public interface ICommand<TInputModel, TResult>
{
    Task<IResult<TResult>> ExecuteAsync(TInputModel inputModel, CancellationToken cancellationToken);
}