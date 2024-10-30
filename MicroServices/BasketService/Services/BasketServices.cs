using AutoMapper;
using BasketServices.Infrastructure;
using BasketServices.MessagingBus;
using BasketServices.Models.Dtos;
using BasketServices.Models.Entities;
using BasketServices.Services.DiscountServices;
using BasketServices.Services.MessageDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BasketServices.Services
{
    public class BasketServices : IBasketServices
    {
        private readonly DataBaseContext dataBaseContext;
        private readonly IMapper mapper;
        private readonly IMassageBus massageBus;
        private string _queuename_BasketCheckout;
        public BasketServices(DataBaseContext dataBaseContext,IMapper mapper, IMassageBus massageBus, IOptions<RabbitMqConfiguration> rabbitMqConfig)
        {
            this.dataBaseContext = dataBaseContext;
            this.mapper = mapper;
            this.massageBus = massageBus;
            _queuename_BasketCheckout = rabbitMqConfig.Value.QueueName_Ordercreated;
        }

        public void AddItemToBasket(AddItemToBaketDto addItem)
        {
            var basket = dataBaseContext.Baskets.Include(p=>p.Items).SingleOrDefault(p=>p.Id==addItem.BasketId);
            if (basket == null)
            {
                throw new Exception("Basket not Found");
            }
            var basketitems = mapper.Map<BasketItems>(addItem);
            basket.Items.Add(basketitems);
            var productDto = new Productdto
            {
                Id = addItem.ProductId,
                Image = addItem.Image,
                ProductName = addItem.ProductName,
                Unitprice = addItem.Unitprice,
            };
            CreateProduct(productDto);
            dataBaseContext.SaveChanges();

        }

        private Productdto GetProduct(string ProductId)
        {
            var product = dataBaseContext.Products.SingleOrDefault(p => p.Id == Guid.Parse(ProductId));
            if (product != null)
            {
                return new Productdto
                {
                    Id = product.Id,
                    Image = product.Image,
                    ProductName = product.ProductName,
                    Unitprice = product.Unitprice,
                };
            }
            return null;

        }
        private Productdto CreateProduct(Productdto Product)
        {
            var product = GetProduct(Product.Id.ToString());
            if (product != null)
            {
                return product;
            }
            else
            {
                var newProduct = mapper.Map<Models.Entities.Product>(Product);
                dataBaseContext.Add(newProduct);
                dataBaseContext.SaveChanges();
                var productDto = mapper.Map<Productdto>(newProduct);
                return productDto;
            }
        }

        public class Productdto
        {
            public Guid Id { get; set; }
            public string ProductName { get; set; }
            public int Unitprice { get; set; }
            public string Image { get; set; }
        }

        public void ApplyDiscountToBasket(string BasketId, string DiscountId)
        {
            var basket = dataBaseContext.Baskets.FirstOrDefault(p => p.Id == Guid.Parse(BasketId));
            if (basket == null)
            {
                throw new Exception("NotFound ...");
            }
            basket.DiscountId = Guid.Parse(DiscountId);
            dataBaseContext.SaveChanges();
        }

        public BasketDto GetBasket(string UserId)
        {
            GetOrCreateBasket( UserId);
            var basket = dataBaseContext.Baskets.Include(p => p.Items).Where(p=>p.UserId==Guid.Parse(UserId)).Select(p => new BasketDto
            {
                Id = p.Id,
                UserId = p.UserId,
                DiscountId = p.DiscountId,
                Items = p.Items.Select(p => new BasketItemDto
                {
                    Id = p.Id,
                    BasketId = p.Id,
                    ProductId = p.Product.Id,
                    ProductName = p.Product.ProductName,
                    Quantity = p.Quantity,
                    Unitprice = p.Product.Unitprice,
                    Image = p.Product.Image
                    
                }).ToList()
            }).FirstOrDefault();
            return basket;
        }

        public Guid GetOrCreateBasket(string UserId)
        {
            var basket = dataBaseContext.Baskets.SingleOrDefault(p => p.UserId == Guid.Parse(UserId));
            if(basket == null)
            {
                var newBasket = new Basket
                {
                    UserId = Guid.Parse(UserId),
                };
                dataBaseContext.Baskets.Add(newBasket);
                dataBaseContext.SaveChanges();
                return newBasket.Id;
            }
            return basket.Id;
        }

        public void RemoveItemFromBasket(Guid itemId)
        {
            var basketItem = dataBaseContext.Items.SingleOrDefault(p=>p.Id == itemId);
            dataBaseContext.Remove(basketItem);
            dataBaseContext.SaveChanges();
        }

        public void SetQuantity(int quantity, Guid ItemId)
        {
            var item = dataBaseContext.Items.SingleOrDefault(p => p.Id == ItemId);
            if(item != null)
            {
                item.SetQuantity(quantity);
            }
            dataBaseContext.SaveChanges();
        }

        public void TransferBasketId(Guid anynumousId, Guid UserId)
        {
            var anynamusBasket = dataBaseContext.Baskets.Include(p=>p.Items).SingleOrDefault(p=>p.UserId == anynumousId);
            if (anynamusBasket == null) return;
            var userBasket = dataBaseContext.Baskets.SingleOrDefault(p=>p.UserId == UserId);
            if(userBasket == null)
            {
                var newUserBasket = new Basket
                {
                    UserId = UserId,
                };
                dataBaseContext.Baskets.Add(newUserBasket);
                dataBaseContext.SaveChanges();
            }
            foreach(var item in anynamusBasket.Items)
            {
                userBasket.Items.Add(item);
            }
            dataBaseContext.SaveChanges();
        }

        public ResultDto Checkout(CheckoutDto checkout, IDiscountServices discountServices)
        {
            var basket = dataBaseContext.Baskets.Include(p=>p.Items).ThenInclude(p=>p.Product).FirstOrDefault(p => p.Id == Guid.Parse(checkout.BasketId));
            if(basket == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "سبد خزید پیدا نشد."
                };
            }
            

            BasketCheckoutMessage message = new BasketCheckoutMessage
            {
                Address = checkout.Address,
                BasketId = basket.Id,
                FirstName = checkout.FirstName,
                LastName = checkout.LastName,
                PhoneNumber = checkout.PhoneNumber,
                PostalCode = checkout.PostalCode,
                UserId = checkout.UserId,

            };
            var totalPrice = 0;
            foreach(var item in basket.Items)
            {
                var basketItem = new BasketItemMessage
                {
                    BasketItemId = item.Id,
                    Name = item.Product.ProductName,
                    Price = item.Product.Unitprice,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };
                totalPrice += item.Product.Unitprice*item.Quantity;
                message.Items.Add(basketItem);
            }
            if (basket.DiscountId != null)
            {
                var discount = discountServices.GetDiscountById(basket.DiscountId.ToString());
                if (discount != null)
                {
                    totalPrice = totalPrice-discount.Data.Amount;
                    
                }      
            }
            message.Totalprice = totalPrice;


            massageBus.SendMassage(message, _queuename_BasketCheckout);

            dataBaseContext.Baskets.Remove(basket);
            dataBaseContext.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = ""
            };
        }
    }


}
