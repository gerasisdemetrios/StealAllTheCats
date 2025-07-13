using StealAllTheCats.Models;

namespace StealAllTheCats.Repositories.Interfaces
{
    public interface ICatsRepository : IRepository<CatEntity>
    {
        Task AddOrUpdate(CatEntity cat);
        Task<IEnumerable<CatEntity>> GetAllPaged(int page = 1, int pageSize = 10, string? tag = null);
    }
}
