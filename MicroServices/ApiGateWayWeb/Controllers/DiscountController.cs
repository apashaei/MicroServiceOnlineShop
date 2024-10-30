using APiGatWayWebApi.Models.Services.DiscountServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APiGatWayWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DiscountController : ControllerBase
    {
        
        private readonly IDiscountServices discountServices;

        public DiscountController(IDiscountServices discountServices)
        {
            this.discountServices = discountServices;
        }
        [HttpGet("Code")]
        public IActionResult Get(string Code)
        {
            var data = discountServices.GetDiscountByCode(Code);
            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetById(string Id)
        {
            var data = discountServices.GetDiscountById(Id);
            return Ok(data);
        }

        [HttpGet("UsedDiscount/{Id}")]
        public IActionResult UsedDiscount(string Id)
        {
            var data = discountServices.UsedDiscount(Id);
            return Ok(data);
        }
    }

   
}
