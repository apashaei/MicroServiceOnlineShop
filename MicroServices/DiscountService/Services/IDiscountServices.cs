using AutoMapper;
using DiscountServices.Infrastructure;
using DiscountServices.Models.Entities;

namespace DiscountServices.Services
{
    public interface IDiscountServices
    {
        DiscountDto GetDiscountByCode(string code);
        DiscountDto GetDiscountById(Guid Id);
        bool UseDiscount(string Id);

        bool AddNewDiscount(DiscountDto discount);
    }

    public class DiscountServices : IDiscountServices
    {
        private readonly DataBaseContext dataBaseContext;
        private readonly IMapper mapper;

        public DiscountServices(DataBaseContext dataBaseContext, IMapper mapper)
        {
            this.dataBaseContext = dataBaseContext;
            this.mapper = mapper;
        }

        public bool AddNewDiscount(DiscountDto discount)
        {
            var newdiscount = new Discount
            {
                Used = false,
                Amount = discount.Amount,
                Code = discount.Code,

            };
            dataBaseContext.Discounts.Add(newdiscount);
            dataBaseContext.SaveChanges();
            return true;
        }

        public DiscountDto GetDiscountByCode(string code)
        {
            var discount = dataBaseContext.Discounts.SingleOrDefault(p=>p.Code == code);
            if (discount == null)
            {
                return null;
            }
            var model = mapper.Map<DiscountDto>(discount);
            return model;

        }

        public DiscountDto GetDiscountById(Guid Id)
        {
            var discount = dataBaseContext.Discounts.SingleOrDefault(p => p.Id == Id);
            if (discount == null)
            {
                return null;
            }
            var model = mapper.Map<DiscountDto>(discount);
            return model;

        }

        public bool UseDiscount(string Id)
        {
            var dscount = dataBaseContext.Discounts.SingleOrDefault(p=>p.Id == Guid.Parse(Id));
            if(dscount == null)
            {
                throw new Exception("Not found");
            }
            dscount.Used = true;
            dataBaseContext.SaveChanges();
            return true;
        }
    }

    public class DiscountDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }

        public bool Used { get; set; }
    }
}
