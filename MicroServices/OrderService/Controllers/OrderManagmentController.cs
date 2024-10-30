using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderSevice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagmentController : ControllerBase
    {

        [HttpGet]
        public IActionResult EditOrder()
        {
            return Ok();
        }
    }
}
