namespace BasketServices.Models.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Unitprice { get; set; }
        public string Image { get; set; }
        public virtual ICollection<BasketItems> BasketItems { get; set; }

   
    }
}
