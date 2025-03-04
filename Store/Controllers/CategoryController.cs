using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper _imapper;
        private readonly IMemoryCache _memoryCache; 

        public CategoriesController(ICategoryService categoryService, IMapper _imapper, IMemoryCache memoryCache) 
        {
            this._imapper = _imapper;
            this.categoryService = categoryService;
            this._memoryCache = memoryCache; 
        }


        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryDTO>>> Get()
        {
       
            const string cacheKey = "categoriesCacheKey";

            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<GetCategoryDTO> categoriesDTO)) 
            {
                IEnumerable<Category> categories = await categoryService.Get();
                categoriesDTO = _imapper.Map<IEnumerable<Category>, IEnumerable<GetCategoryDTO>>(categories);

                var cacheEntryOptions = new MemoryCacheEntryOptions() 
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5)); 

                _memoryCache.Set(cacheKey, categoriesDTO, cacheEntryOptions); 
            }

            if (categoriesDTO != null)
                return Ok(categoriesDTO);

            return NoContent();

        }
    }
}
