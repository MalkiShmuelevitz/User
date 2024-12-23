using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Services;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService productService;
        IMapper _imapper;

        public ProductsController(IProductService productservice, IMapper _imapper)
        {
            this._imapper = _imapper;
            this.productService = productservice;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get(int position, int skip, string? desc, int? minPrice, int? maxPrice,
            int?[] categoryIds)
        {
            IEnumerable<Product> products = await productService.Get(position, skip, desc, minPrice, maxPrice, categoryIds);
            IEnumerable<ProductDTO> productsDTO = _imapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
            return Ok(productsDTO);
        }

        
    }
}
