using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService categoryService;
        IMapper _imapper;

        public CategoriesController(ICategoryService categoryService,IMapper _imapper)
        {
            this._imapper = _imapper;
            this.categoryService = categoryService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<getCategoryDTO>>> Get()
        {
            IEnumerable<Category> categories = await categoryService.Get();
            IEnumerable<getCategoryDTO> categoriesDTO = _imapper.Map<IEnumerable<Category>, IEnumerable<getCategoryDTO>>(categories);
            if(categoriesDTO!=null)
                return Ok(categoriesDTO);
            return NoContent();

        }
    }
}
