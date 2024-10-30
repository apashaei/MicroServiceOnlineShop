using BasketServices.Models.Entities;

namespace BasketServices.Services
{
    public class BasketItemDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public Guid ProductId { get; set; }
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }
        public int Unitprice { get; set; }
        public int Quantity { get; set; }
        public string Image {  get; set; }
        
    }


}
