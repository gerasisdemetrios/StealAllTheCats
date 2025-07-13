using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Models;
using StealAllTheCats.Models.Dto;
using StealAllTheCats.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery][Required] int page = 1, [FromQuery][Required] int pageSize = 10, string? tag = null)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest("Page and pageSize must be greater than 0.");

            var items = await _catsService.GetAllPaged(page, pageSize, tag);

            var itemsDto = _mapper.Map<List<CatDto>>(items.ToList());

            var totalCount = await _catsService.GetCatsCount();

            var pagedItems = itemsDto
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();   

            var result = new
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Items = pagedItems
            };

            return Ok(result);
        }
    }
}
