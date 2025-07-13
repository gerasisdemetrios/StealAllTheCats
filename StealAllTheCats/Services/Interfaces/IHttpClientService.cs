using StealAllTheCats.Models;
using StealAllTheCats.Models.Api;

namespace StealAllTheCats.Services.Interfaces
{
    public interface IHttpClientService
    {
        public Task<List<CatApiModel>> FetchCatsAsync();
    }
}
