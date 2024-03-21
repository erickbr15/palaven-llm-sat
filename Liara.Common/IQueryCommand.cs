namespace Liara.Common;

public interface IQueryCommand<TSearchCriteria, TResult>
{
    IResult<TResult> Search(TSearchCriteria criteria);
}
