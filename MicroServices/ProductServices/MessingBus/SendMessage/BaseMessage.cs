namespace ProductServices.MessingBus.SendMessage
{
    public class BaseMessage
    {
        public Guid MessageId { get; set; }= Guid.NewGuid();
        public DateTime CreateDate { get; set; }= DateTime.Now;
    }
}
