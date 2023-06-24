using System.Linq.Expressions;

namespace Transflo.Assessment.Core.Interfaces.Repositories
{
    public interface IReadonlyRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>>? includes = null);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    }
}
