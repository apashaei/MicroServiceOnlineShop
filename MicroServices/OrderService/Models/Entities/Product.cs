namespace OrderSevice.Models.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }

        public ICollection<Orderline> Orders { get; set; }
    }
}
