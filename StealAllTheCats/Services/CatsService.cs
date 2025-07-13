using AutoMapper;
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
                await _catsRepository.AddOrUpdateAsync(catEntity);
            }

            var catEntities = await _catsRepository.GetAllAsync();

            return catEntities.ToList();
        }
    
        public async Task<CatEntity> GetCatById(int id)
        {
            var catEntity = await _catsRepository.GetByIdAsync(id);

            return catEntity;
        }
    }
}
