namespace BasketServices.Services
{
    public class BasketDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public Guid? DiscountId { get; set; }
    }


}
