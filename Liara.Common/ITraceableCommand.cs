namespace Liara.Common;

public interface ITraceableCommand<TInputModel, TResult>
{
    Task<IResult<TResult>> ExecuteAsync(Guid traceId, TInputModel inputModel, CancellationToken cancellationToken);
}
