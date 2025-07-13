using StealAllTheCats.Models;

namespace StealAllTheCats.Services.Interfaces
{
    public interface ICatsService
    {
        public Task<IList<CatEntity>> FetchCats();

        public Task<CatEntity> GetCatById(int id);

        public Task<IEnumerable<CatEntity>> GetAllPaged(int page = 1, int pageSize = 10, string? tag = null);

        public Task<int> GetCatsCount();
    }
}
