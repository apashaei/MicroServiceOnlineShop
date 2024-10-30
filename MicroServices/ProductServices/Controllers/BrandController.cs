using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Services.BransServices;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;
        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await brandService.Getbrand();
            if(data.Success)
                return Ok(data);
            return BadRequest(data);
        }
    }
}
