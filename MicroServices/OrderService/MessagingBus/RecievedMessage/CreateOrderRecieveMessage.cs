
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderSevice.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json.Serialization;

namespace OrderSevice.MessagingBus.RecievedMessage
{
    public class CreateOrderRecieveMessage : BackgroundService
    {
        private RabbitMQ.Client.IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly string _QueueName;
        private readonly IOrderServices orderServices;

        public CreateOrderRecieveMessage(IOptions<RabbotMQConfig> rabbitMqConfig, IOrderServices orderServices)
        {
            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            _QueueName = rabbitMqConfig.Value.QueueName_Ordercreated;

            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password,
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(_QueueName,
                        durable: false, exclusive: false, autoDelete: false, arguments: null);
            this.orderServices = orderServices;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var body = Encoding.UTF8.GetString(args.Body.ToArray());
                var basket = JsonConvert.DeserializeObject<BasketDto>(body);

                var result = HandelMessage(basket);
                if (result)
                {

                    _channel.BasicAck(args.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(_QueueName,false, consumer);

            return Task.CompletedTask;
        }

        private bool HandelMessage(BasketDto basket)
        {

            return  orderServices.RegisterOrder(basket);
             
        }
    }

    public class Item
    {
        public string BasketItemId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }

    public class BasketDto
    {
        public string BasketId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string UserId { get; set; }
        public int Totalprice { get; set; }
        public List<Item> Items { get; set; }
        public string MessageId { get; set; }
        public DateTime CreationTime { get; set; }
    }

}
