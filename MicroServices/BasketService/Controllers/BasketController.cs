using BasketServices.Services;
using BasketServices.Services.DiscountServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace BasketServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BasketController : ControllerBase
    {
        private readonly IBasketServices basketServices;

        public BasketController(IBasketServices basketServices)
        {
            this.basketServices = basketServices;
        }

        [HttpGet]
        public IActionResult Get(string UserId)
        {
            var data  = basketServices.GetBasket(UserId);
            return Ok(data);
        }

        [HttpGet("GetOrCreate")]
        public IActionResult GetOrCreate(string UserId)
        {
            var data = basketServices.GetOrCreateBasket(UserId);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult additemToBasket(AddItemToBaketDto addItem)
        {
            var basket = basketServices.GetOrCreateBasket(addItem.UserId);
            addItem.BasketId = basket;
            basketServices.AddItemToBasket(addItem);
            var basketDats = basketServices.GetBasket(addItem.UserId);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(string ItemId)
        {
            basketServices.RemoveItemFromBasket(Guid.Parse(ItemId));
            return Ok();
        }
        [HttpPut]
        public IActionResult SetQuantity(int quantity, string itemId)
        {
            basketServices.SetQuantity(quantity, Guid.Parse(itemId));
            return Ok();
        }

        [HttpPut("{BasketId}/{DiscountId}")]
        public IActionResult ApplyDiscountOnBasket(string BasketId, string DiscountId)
        {
            basketServices.ApplyDiscountToBasket(BasketId, DiscountId);
            return Accepted();
        }

        [HttpPost("CheckoutBasket")]

        public IActionResult CheckoutBasket(CheckoutDto checkout, [FromServices]IDiscountServices discountServices)
        {
            var data = basketServices.Checkout(checkout,discountServices);
            if(data.IsSuccess)
            {
                return Ok(data);
            }
            return BadRequest();
        }


    }
}
