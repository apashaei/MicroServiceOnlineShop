namespace Microservice.Web.FronEnd.Services.BasketServices
{
    public class AddItemToBasketDto
    {
        public string productName { get; set; }
        public string productId { get; set; }
        public string basketId { get; set; }
        public int unitprice { get; set; }
        public int quantity { get; set; }
        public string userId { get; set; }

        public string Image { get; set; }
    }

   
}
