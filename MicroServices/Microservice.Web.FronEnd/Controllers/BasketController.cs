using Microservice.Web.FronEnd.Models.NewFolder;
using Microservice.Web.FronEnd.Services.BasketServices;
using Microservice.Web.FronEnd.Services.DiscountServices;
using Microservice.Web.FronEnd.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Microservice.Web.FronEnd.Controllers
{

    public class BasketController : Controller
    {
        private readonly IBasketServices basket;
        private readonly IproductSevice productSevice;
        //private readonly IDiscountServices discountServices;
        private readonly IDiscountServicesRestfull discountServices;
        private string UserId { get; set; }

        public BasketController(IBasketServices basket, IproductSevice productSevice, IDiscountServicesRestfull discountServices)
        {
            this.basket = basket;
            this.productSevice = productSevice;
            this.discountServices = discountServices;
        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                SetOrGetCookie();
            }
            else
            {
                UserId = User.Claims.FirstOrDefault(p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            }
            var data = basket.GetBasket(UserId);

            if(data.discountId != null)
            {
                var discount = discountServices.GetDiscountById(data.discountId.ToString());
                data.DiscountDetail = new DiscountDetail
                {
                    Amount = discount.Data.Amount,
                    Code = discount.Data.Code,
                };
            }

            return View(data);
        }

        public IActionResult Delete(string Id)
        {
            var data = basket.RemoveItemFromBasket(Guid.Parse(Id));
            return RedirectToAction("Index");
        }
        public IActionResult AddTobasket(string id)
        {
            var product = productSevice.GetProductById(Guid.Parse(id));
            if (!User.Identity.IsAuthenticated)
            {
                SetOrGetCookie();
            }
            else
            {
                UserId = User.Claims.FirstOrDefault(p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            }
            var basketResult = basket.GetOrCreateBasket(UserId);
            AddItemToBasketDto addItem = new AddItemToBasketDto()
            {
                basketId = basketResult,
                productId = id,
                productName = product.name,
                quantity = 1,
                unitprice = product.price,
                userId = UserId,
                Image = product.image,

            };
            basket.AddItemToBasket(addItem);
            return RedirectToAction("index");
        }

        private void SetOrGetCookie()
        {
            string BasketCookieName = "BasketId";
            if (Request.Cookies.ContainsKey(BasketCookieName))
            {
                UserId = Request.Cookies[BasketCookieName];
            }
            else
            {
                UserId = Guid.NewGuid().ToString();
                var cookieoptions = new CookieOptions { IsEssential = true };
                cookieoptions.Expires = DateTime.Now.AddYears(2);
                Response.Cookies.Append(BasketCookieName, UserId, cookieoptions);
            }
        }

        public IActionResult Edit(int quantity, Guid BasketItemId)
        {
            basket.SetQuantity(quantity, BasketItemId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ApplyDiscountOnBasket(string DiscountCode)
        {
            if (DiscountCode == null)
            {
                return Json(new ResultDto
                {
                    IsSuccess = false,
                    Message = "لطفا کد تخفیف را وارد کنید."
                });
            }
            var discount = discountServices.GetDiscountByCode(DiscountCode);
            if (discount.IsSuccess)
            {
                if (discount.Data.Used)
                {
                    return Json(new ResultDto
                    {
                        IsSuccess = false,
                        Message = "کد تخفیف قبلا استفاده شده است"
                    });
                }
                string UserId = User.Claims.FirstOrDefault(p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                var userBasket = basket.GetBasket(UserId);
                if (userBasket != null)
                {
                    basket.ApplyBasketOnDiscount(userBasket.id, discount.Data.Id.ToString());
                    discountServices.UsedDiscount(discount.Data.Id.ToString());
                    return Json(new ResultDto
                    {
                        IsSuccess = false,
                        Message = "کد تخفیف با موفقیت اعمال شد."
                    });
                }
            }
            return Json(new ResultDto
            {
                IsSuccess = false,
                Message = "کد تحفیف وجود ندارد."
            });

        }

        [Authorize]
        public IActionResult GetBasketWithProducts()
        {
            string UserId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            var result =basket.GetProductBasket(UserId);
            return View();


        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutDto checkout)
        {
            checkout.UserId = User.Claims.FirstOrDefault(p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value; ;
            checkout.BasketId = (basket.GetBasket(checkout.UserId).id);
            var data = basket.CheckoutBasket(checkout);
            if(data.IsSuccess)
            {
                return RedirectToAction("CheckoutSucces");
            }
            return View(checkout);

        }
        public IActionResult CheckoutSucces()
        {
            return View();
        }
    }
}
