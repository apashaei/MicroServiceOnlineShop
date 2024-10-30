using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSevice.Services;

namespace OrderSevice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPayController : ControllerBase
    {
        private readonly IOrderServices orderServices;

        public OrderPayController(IOrderServices orderServices)
        {
            this.orderServices = orderServices;
        }

        [HttpPost]
        public IActionResult Post(Guid orderId)
        {
            return Ok(orderServices.SendOrderToPay(orderId));
        }
    }
}
