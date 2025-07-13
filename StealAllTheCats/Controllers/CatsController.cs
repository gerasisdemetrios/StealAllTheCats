using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StealAllTheCats.Models;
using StealAllTheCats.Models.Dto;
using StealAllTheCats.Services.Interfaces;

namespace StealAllTheCats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController : ControllerBase
    {
        private readonly ICatsService _catsService;
        private readonly IMapper _mapper;
        public CatsController(ICatsService catsService, IMapper mapper)
        {
            _catsService = catsService;
            _mapper = mapper;
        }

        [HttpPost("fetch")]
        public async Task Fetch()
        {
            await _catsService.FetchCats();
        }

        [HttpGet("{id}")]
        public async Task<CatDto> Get(int id)
        {
            var catEntity = await _catsService.GetCatById(id);

            var catDto = _mapper.Map<CatDto>(catEntity);

            return catDto;
        }
    }
}
