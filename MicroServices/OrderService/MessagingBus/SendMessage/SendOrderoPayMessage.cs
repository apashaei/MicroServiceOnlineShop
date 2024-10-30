namespace OrderSevice.MessagingBus.SendMessage
{
    public class SendOrderoPayMessage: BaseMessage
    {
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
    }
}
