using Microsoft.AspNetCore.Mvc;
using StealAllTheCats.Models;
using StealAllTheCats.Services.Interfaces;

namespace StealAllTheCats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController : ControllerBase
    {
        ICatsService _catsService;
        public CatsController(ICatsService catsService)
        {
            _catsService = catsService;
        }

        [HttpGet("fetch")]
        public async Task<IList<CatEntity>> Get()
        {
            return await _catsService.FetchCats();
        }        
    }
}
