using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderSevice.Infrastructure;
using OrderSevice.MessagingBus;
using OrderSevice.MessagingBus.RecievedMessage;
using OrderSevice.MessagingBus.SendMessage;
using OrderSevice.Models.Dtos;
using OrderSevice.Models.Entities;
using OrderSevice.Services.Product;

namespace OrderSevice.Services
{
    public interface IOrderServices
    {
        //void AddOrder(AddOrderDto addOrder);
        List<OrderDto> GetUserOrders(string UserId);
        OrderDetailDto GetOrderById(Guid OrderId);

        bool RegisterOrder(BasketDto Basket);
        ResultDto SendOrderToPay(Guid OrderId);

    }

    public class OrderServices : IOrderServices
    {
        private readonly DataBaseContext context;
        private readonly IProductService productService;
        private readonly RabbitMQSendMessage _rabbitMQSendMessage;
        private readonly RedisSendMessage _redisSendMessage;
        private string QueueName;

        public OrderServices(DataBaseContext context, IProductService productService,IMassageBus massageBus, IOptions<RabbotMQConfig> rabbitMqConfig,
            RabbitMQSendMessage rabbitMQSendMessage,
            RedisSendMessage redisSendMessage)
        {
            this.context = context;
            this.productService = productService;
            _rabbitMQSendMessage = rabbitMQSendMessage;
            _redisSendMessage = redisSendMessage;
            QueueName = rabbitMqConfig.Value.QueueName_SendOrderToPayment;
        }

        public OrderDetailDto GetOrderById(Guid OrderId)
        {
            var order = context.Orders.Include(p=>p.Orderlines).ThenInclude(p=>p.Product).Where(p=>p.Id == OrderId).Select(p=> new OrderDetailDto
            {
                Id = p.Id,
                 Address = p.Address,
                 FirstName = p.FirstName,
                 LastName = p.LastName,
                 Orderpaid = p.Orderpaid,
                 Orderplaced = p.Orderplaced,
                 PhoneNumber = p.PhoneNumber,
                 UserId = p.UserId,
                 Paymetnstatus = p.Paymetnstatus,
                 
                orderlines = p.Orderlines.Select(o=> new OrderLineDto
                {
                    Id=o.Id,
                    ProducId = o.Product.ProductId,
                    ProductName = o.Product.ProductName,
                    Quantity = o.Quantity,
                    UnitPrice = o.Product.Price,
                    
                    
                }).ToList(),
                TotalPrice = p.Orderlines.Sum(p=>p.Quantity* p.Product.Price)
            }).SingleOrDefault();
            return order;
        }

        public List<OrderDto> GetUserOrders(string UserId)
        {
            
            var orders = context.Orders.Include(p=>p.Orderlines).Where(p=>p.UserId == UserId).Select(p=> new OrderDto
            {
                OrderId = p.Id,
                ItemCount = p.Orderlines.Count,
                Orderpaid = p.Orderpaid,
                Orderplaced = p.Orderplaced,
                TotalPrice = p.TotalPrice,
                Paymetnstatus = p.Paymetnstatus
            }).ToList();
            return orders;
        }

        public bool RegisterOrder(BasketDto Basket)
        {
            List<Orderline> items = new List<Orderline>();
            var totalPrice = 0;
            var updateProductSellNumber = new UpdateSellNumberMessafgeDto();
           
            foreach(var item in Basket.Items)
            {
                var product = productService.GetProduct(new ProductDto
                {
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductName = item.Name
                });
                var newOrderLine = new Orderline
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    Product = product,
                    Quantity = item.Quantity,
                };
                items.Add(newOrderLine);
                totalPrice += item.Price*item.Quantity;
                updateProductSellNumber.ProductIds.Add(item.ProductId);
            }

            var newOrder = new Orders(Basket.UserId,
                orderlines: items,
                Basket.FirstName,
                Basket.LastName,
                Basket.Address,
                Basket.PhoneNumber);
            newOrder.TotalPrice = totalPrice;
            context.Add(newOrder);

            _redisSendMessage.SendMassage(updateProductSellNumber, "updateProductSellNumber");


            context.SaveChanges();
            return true;
        }

        public ResultDto SendOrderToPay(Guid OrderId)
        {
            var order = context.Orders.FirstOrDefault(p=>p.Id==OrderId);
            if(order == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "یافت نشد."
                };
            }
            SendOrderoPayMessage payMessage = new SendOrderoPayMessage
            {
                Amount = order.TotalPrice,
                OrderId = order.Id,
            };
            _rabbitMQSendMessage.SendMassage(payMessage, QueueName);

            order.RequestPay();
            context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "",
            };
            
        }
    }

    public class SendOrderToPayDto
    {
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
    }

    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime Orderplaced { get; set; }

        public bool Orderpaid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalPrice { get; set; }
        public Paymetnstatus Paymetnstatus { get; set; }
     
        public List<OrderLineDto> orderlines {  get; set; }


    }


    public class OrderLineDto
    {
        public Guid Id { get; set; }
        public Guid ProducId { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }

    }

    public class OrderDto
    {
        public Guid OrderId { get; set; }
        
        public DateTime Orderplaced { get; set; }

        public bool Orderpaid { get; set; }
        public int ItemCount { get; set; }
        public int TotalPrice { get; set; }
        public Paymetnstatus Paymetnstatus{ get; set; }

        
    }

    public class UpdateSellNumberMessafgeDto : BaseMessage
    {
        public List<Guid> ProductIds { get; set; }
    }
}
