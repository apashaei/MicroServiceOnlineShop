using Microservice.Web.FronEnd.Services.BasketServices;
using Microservice.Web.FronEnd.Services.OrderServices;
using Microservice.Web.FronEnd.Services.PaymentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Web.FronEnd.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderServices order;
        private readonly IPaymentServices paymentServices;

        public OrderController(IOrderServices order, IPaymentServices paymentServices)
        {
            this.order = order;
            this.paymentServices = paymentServices;
        }
        public IActionResult Index()
        {

            string UserId = User.Claims.FirstOrDefault(p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            var data = order.GetOrders().Result;
            return View(data);
        }

        public IActionResult Detail(Guid Id)
        {
            var data = order.GetOrderById(Id);
            return View(data);
        }

        public IActionResult Pay(Guid Id)
        {
            var orderForPay = order.GetOrderById(Id).Result;
            if (orderForPay.Paymetnstatus == Paymetnstatus.NotPaid)
            {
                var result = order.SendOrderToPay(Id).Result;
                if (result.IsSuccess)
                {
                    string callBackUrl = Url.Action(nameof(Detail), "Order", new { Id = Id }, protocol: Request.Scheme);
                    var payResult = paymentServices.SentToPayment(Id, callBackUrl);
                    if (payResult.IsSuccess)
                    {
                        return Redirect(payResult.Data);
                    }
                }
            }
            return RedirectToAction("Detail");

        }
    }
}
