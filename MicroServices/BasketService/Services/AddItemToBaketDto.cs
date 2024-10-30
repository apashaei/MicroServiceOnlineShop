namespace BasketServices.Services
{
    public class AddItemToBaketDto
    {
        
        public string ProductName { get; set; }
        public Guid ProductId { get; set; }
        public Guid BasketId { get; set; }
        public int Unitprice { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public string Image {  get; set; }
    }


}
