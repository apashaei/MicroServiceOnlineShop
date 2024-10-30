using Microsoft.AspNetCore.Mvc;
using ProductServices.Models.Dtos;
using ProductServices.Services.CategoryServices;
using ProductServices.Services.ProductServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices categoryServices;
        private readonly IProductServices productServices;

        public CategoryController(ICategoryServices categoryServices, IProductServices productServices)
        {
            this.categoryServices = categoryServices;
            this.productServices = productServices;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        [ProducesResponseType(typeof(ResultDto<List<GetCategoriesDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid? ParentId)
        {
            var data = await categoryServices.GetCategory(ParentId);
            return Ok(data);
        }

        // POST api/<CategoryController>
        [HttpPost]
        [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(string Name, Guid? ParentId)
        {
            var result = await categoryServices.AddNewCategory(Name, ParentId);
            return Ok(result);
        }

        [HttpPost("AddChild")]
        [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(string Name, string Link, Guid ParentId)
        {
            var result = await categoryServices.AddNewChidToCategort(Name,Link, ParentId);
            return Ok(result);
        }


        [HttpGet("GeDrpCategories")]
        [ProducesResponseType(typeof(ResultDto<List<GetDrpCategories>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDto<List<GetDrpCategories>>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GeDrpCategories()
        
        {
            
            var data = await categoryServices.GetDrpCategories();
            return Ok(data);
        }

    }
}
