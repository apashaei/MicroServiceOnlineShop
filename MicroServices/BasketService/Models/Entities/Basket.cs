namespace BasketServices.Models.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid? DiscountId { get; set; }
        public virtual ICollection<BasketItems> Items { get; set; }
       
    }
}
