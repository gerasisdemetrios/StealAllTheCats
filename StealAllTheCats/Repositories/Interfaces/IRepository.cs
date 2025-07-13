using System.Linq.Expressions;

namespace StealAllTheCats.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> SaveAsync(T entity);
        public Task RemoveAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task<T> GetByQueryAsync(Expression<Func<T, bool>> predicate);

        public Task<IEnumerable<T>> GetAllByQueryAsync(Expression<Func<T, bool>> predicate);
        public Task<int> GetCatsCount();
    }
}
