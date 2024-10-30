namespace Microservice.Web.FronEnd.Services.BasketServices
{
    public class ItemsDto
    {
        public string id { get; set; }
        public string productName { get; set; }
        public string productId { get; set; }
        public string basketId { get; set; }
        public object basket { get; set; }
        public int unitprice { get; set; }
        public int quantity { get; set; }



        public int Totalprice()
        {
            return quantity*unitprice;
        }
    }

   
}
