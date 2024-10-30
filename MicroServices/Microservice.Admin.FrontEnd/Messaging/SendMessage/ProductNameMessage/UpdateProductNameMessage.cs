namespace Microservice.Admin.FrontEnd.Messaging.SendMessage.ProductNameMessage
{
    public class UpdateProductNameMessage:BaseMessage
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
    }
}
