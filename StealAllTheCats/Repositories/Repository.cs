using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using StealAllTheCats.Repositories.Interfaces;
using System.Linq.Expressions;

namespace StealAllTheCats.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CatsDBContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(CatsDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T> SaveAsync(T entity)
        {
            var createdEntity = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return createdEntity.Entity;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByQueryAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
