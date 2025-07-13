using System.Linq.Expressions;

namespace StealAllTheCats.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> SaveAsync(T entity);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByQueryAsync(Expression<Func<T, bool>> predicate);
    }
}
