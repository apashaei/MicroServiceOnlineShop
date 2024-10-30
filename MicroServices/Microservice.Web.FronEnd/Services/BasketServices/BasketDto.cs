namespace Microservice.Web.FronEnd.Services.BasketServices
{
    public class BasketDto
    {
        public string id { get; set; }
        public string userId { get; set; }
        public Guid? discountId { get; set; }
        public DiscountDetail DiscountDetail { get; set; }
        public List<ItemsDto> items { get; set; }

        public int TotalPrice()
        {
            if (discountId != null)
            {
                return items.Sum(p => p.unitprice * p.quantity) - DiscountDetail.Amount;
            }
            else
            {
                return items.Sum(p => p.unitprice * p.quantity);
            }
            
        }


    }

    public class DiscountDetail
    {
        public int Amount { get; set; }
        public string Code { get; set; }

    }
}
