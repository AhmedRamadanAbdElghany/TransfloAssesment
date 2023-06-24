using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Transflo.Assessment.Core.Interfaces.Repositories;

namespace Transflo.Assessment.Infrastructure.Persistence.Repositories
{
    internal class ReadonlyRepository<TEntity> : IReadonlyRepository<TEntity> where TEntity : class
    {
        private readonly TransfloDBContext _context;

        public ReadonlyRepository(TransfloDBContext context)
        {
            _context = context;
        }
        private IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking().AsQueryable();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>>? includes = null)
        {
            IQueryable<TEntity>? query = GetAll();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null && includes.Any())
            {
                foreach (Expression<Func<TEntity, object>>? include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();

        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression);
        }
    }
}
