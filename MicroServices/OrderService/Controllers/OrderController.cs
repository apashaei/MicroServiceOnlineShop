using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSevice.Services;

namespace OrderSevice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices order;

        public OrderController(IOrderServices order)
        {
            this.order = order;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string UserId = User.Claims.FirstOrDefault(p=>p.Type== "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var data = order.GetUserOrders(UserId);
            return Ok(data);
        }

        [HttpGet("OrderId")]
        public IActionResult Get(Guid OrderId)
        {
            var data = order.GetOrderById(OrderId);
            return Ok(data);
        }
       

      
    }
}
