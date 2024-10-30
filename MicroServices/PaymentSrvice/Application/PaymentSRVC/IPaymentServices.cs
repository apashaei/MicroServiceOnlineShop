using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PaymentSRVC
{
    public interface IPaymentServices
    {
        PaymentDto GetPaymentOfOrder(Guid OrderId);
        PaymentDto GetPayment(Guid PaymentId);
        bool CreatePayment(Guid OrderId, int Amount);

        void PayDone(Guid PaymentId, string Authority, long RefId);
    }

    public class PaymentServices : IPaymentServices
    {
        private readonly IDatabaseContext context;

        public PaymentServices(IDatabaseContext context)
        {
            this.context = context;
        }
        public bool CreatePayment(Guid OrderId, int Amount)
        {
            var order = GetOrder(OrderId, Amount);
            var newPayment = new Payment()
            {
                Amount = order.Amount,
                Order = order,
                IsPay = false,
                Authority = "",
                RefId=""

            };
            context.Payments.Add(newPayment);
            context.SaveChanges();
            return true;
        }

        private Order GetOrder(Guid OrderId, int Amount)
        {
            var order = context.Orders.FirstOrDefault(p=>p.OrderId == OrderId);
            if(order != null)
            {
                return order;
            }
            var newOrder = new Order()
            {
                Amount = Amount,
                OrderId = OrderId
            };
            context.Orders.Add(newOrder);
            context.SaveChanges();
            return newOrder;
        }

        public PaymentDto GetPayment(Guid PaymentId)
        {
            var payment = context.Payments.FirstOrDefault(p=>p.Id == PaymentId);
            if (payment != null)
            {
                throw new Exception("not Found");
            }
            return new PaymentDto
            {
                Id = PaymentId,
                Amount = payment.Amount,
                Authority = payment.Authority,
                DatePay = payment.DatePay,
                IsPay = payment.IsPay,
                orderId = payment.orderId
            };
        }

        public PaymentDto GetPaymentOfOrder(Guid OrderId)
        {
            var payment = context.Payments.FirstOrDefault(p=>p.orderId == OrderId);
            return new PaymentDto
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Authority = payment.Authority,
                DatePay = payment.DatePay,
                IsPay = payment.IsPay,
                orderId = payment.orderId
            };
        }

        public void PayDone(Guid PaymentId, string Authority, long RefId)
        {
            var payment = context.Payments.FirstOrDefault(p => p.Id == PaymentId);
            if (payment != null)
            {
                throw new Exception("not Found");
            }

            payment.Authority = Authority;
            payment.RefId = RefId.ToString();
            payment.DatePay = DateTime.Now;
            payment.IsPay = true;
            context.SaveChanges();
        }
    }

    public class PaymentDto
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime DatePay { get; set; }
        public string Authority { get; set; }
        public string RefId { get; set; }

        public Guid orderId { get; set; }
    }
}
