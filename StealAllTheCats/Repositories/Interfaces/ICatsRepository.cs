using StealAllTheCats.Models;

namespace StealAllTheCats.Repositories.Interfaces
{
    public interface ICatsRepository : IRepository<CatEntity>
    {
        Task AddOrUpdateAsync(CatEntity cat);
    }
}
