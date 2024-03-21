using System.Linq.Expressions;

namespace Liara.Common.DataAccess;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties);
    IQueryable<TEntity> GetAll();
    bool Exists(object id);
    Task<bool> ExistsAsync(object id, CancellationToken cancellationToken);
    TEntity? GetById(object id);
    Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken);
    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
