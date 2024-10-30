using ProductServices.MessingBus.SendMessage;

namespace ProductServices.Models.Dtos
{
    public class UpdateProductMessageDto:BaseMessage
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
    }
}
