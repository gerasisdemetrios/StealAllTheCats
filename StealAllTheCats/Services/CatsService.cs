using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StealAllTheCats.Models;
using StealAllTheCats.Repositories.Interfaces;
using StealAllTheCats.Services.Interfaces;

namespace StealAllTheCats.Services
{
    public class CatsService : ICatsService
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientService _httpClientService;
        private readonly ICatsRepository _catsRepository;
        public CatsService(IHttpClientService httpClientService,
            IMapper mapper,
            ICatsRepository catsRepository)
        {
            _httpClientService = httpClientService;
            _catsRepository = catsRepository;
            _mapper = mapper;
        }

        public async Task<IList<CatEntity>> FetchCats()
        {
            var cats = await _httpClientService.FetchCatsAsync();

            foreach (var cat in cats)
            {
                CatEntity catEntity = _mapper.Map<CatEntity>(cat);
                await _catsRepository.AddOrUpdate(catEntity);
            }

            var catEntities = await _catsRepository.GetAllAsync();

            return catEntities.ToList();
        }
    
        public async Task<CatEntity> GetCatById(int id)
        {
            var catEntity = await _catsRepository.GetByIdAsync(id);

            return catEntity;
        }

        public async Task<IEnumerable<CatEntity>> GetAllPaged(int page = 1, int pageSize = 10, string? tag = null)
        {
            var pagedCats = await _catsRepository.GetAllPaged(page, pageSize, tag);

            return pagedCats;
        }

        public async Task<int> GetCatsCount()
        {
            return await _catsRepository.GetCatsCount();
        }
    }
}
