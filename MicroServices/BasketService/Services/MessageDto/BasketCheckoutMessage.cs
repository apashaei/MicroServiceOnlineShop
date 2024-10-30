using BasketServices.MessagingBus;

namespace BasketServices.Services.MessageDto
{
    public class BasketCheckoutMessage:BaseMessage
    {
        public Guid BasketId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string UserId { get; set; }
        public int Totalprice { get; set; }
        public List<BasketItemMessage> Items { get; set; } = new List<BasketItemMessage>();
    }
    public class BasketItemMessage
    {
        public Guid BasketItemId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
