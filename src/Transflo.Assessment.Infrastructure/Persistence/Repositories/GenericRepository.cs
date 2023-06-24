using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Transflo.Assessment.Core.Interfaces.Repositories;

namespace Transflo.Assessment.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly TransfloDBContext _context;

        public GenericRepository(TransfloDBContext context)
        {
            _context = context;
        }
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            int result = await _context.SaveChangesAsync();
            return result >= 0;
        }
        public void Attach(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
        }
    }
}
