using StealAllTheCats.Models;

namespace StealAllTheCats.Services.Interfaces
{
    public interface ICatsService
    {
        public Task<IList<CatEntity>> FetchCats();

        public Task<CatEntity> GetCatById(int id);
    }
}
