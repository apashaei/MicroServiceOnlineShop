using Application.PaymentSRVC;
using Dto.Payment;
using Infrastructure.Messaging.Configs;
using Infrastructure.Messaging.SendMessage.SendPaDoneMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payment.EndPoint.Models.Dtos;
using ZarinPal.Class;

namespace Payment.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PayController : ControllerBase
    {
        private readonly IPaymentServices paymentServices;
        private readonly IMessageBus messageBus;
        private readonly ZarinPal.Class.Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        private string queueName;

        public PayController(IPaymentServices paymentServices, IMessageBus messageBus, IOptions<RabbitMQConfig> rabbitMqConfig)
        {
            this.paymentServices = paymentServices;
            this.messageBus = messageBus;
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            queueName = rabbitMqConfig.Value.QueueName_SendPayinfo;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid OrderId, string CallBackUrl)
        {
            var payment = paymentServices.GetPaymentOfOrder(OrderId);
            if (payment == null)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message = "پرداخت یافت نشد."
                });

            }
            if (payment.Amount > 0)
            {
                var calllBackUrll = Url.Action(nameof(Verify), "Pay", new { PaymentId = payment.Id, CallBackUrlFront = CallBackUrl }, protocol: Request.Scheme);

                var result = await _payment.Request(new DtoRequest()
                {
                    CallbackUrl = calllBackUrll,
                    Description = "test",
                    Email = "",
                    Amount = payment.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                return Ok(new ResultDto<string>
                {
                    IsSuccess = true,
                    Data = $"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}"
                });

            }
            return NotFound();

        }

        [HttpGet("Verify")]
        public async Task<IActionResult> Verify(string PaymentId, string CallBackUrlFront, string authority, string Status)
        {
            if (Status != "" & authority != "" & Status.ToString().ToLower() == "ok")
            {
                var requestPay = paymentServices.GetPayment(Guid.Parse(PaymentId));
                var verification = await _payment.Verification(new DtoVerification
                {
                    Amount = requestPay.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                    Authority = authority
                }, ZarinPal.Class.Payment.Mode.sandbox);

                if (verification.Status == 100)
                {
                    paymentServices.PayDone(Guid.Parse(PaymentId), authority, verification.RefId);


                    //ارسال پیام پرداخت به order.
                    PayDoneMessageDto payDone = new PayDoneMessageDto
                    {
                        OrderId = requestPay.orderId,
                        PayDone = true,
                    };
                    messageBus.SendMessage(payDone, queueName);

                    return Redirect(CallBackUrlFront);
                }
                else
                {
                    return NotFound(CallBackUrlFront);
                }
                
            }
            else
            {
                return NotFound(CallBackUrlFront);
            }

        }
    }
}
