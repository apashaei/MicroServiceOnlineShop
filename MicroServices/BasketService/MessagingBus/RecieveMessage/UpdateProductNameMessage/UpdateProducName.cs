
using BasketServices.Services.Product;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace BasketServices.MessagingBus.RecieveMessage.UpdateProductNameMessage
{
    public class UpdateProducName : BackgroundService
    {
        private RabbitMQ.Client.IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly string _QueueName;
        private readonly string _exchangeName;
        private readonly IProductServices product;

        public UpdateProducName(IOptions<RabbitMqConfiguration> rabbitMqConfig, IProductServices product)
        {
            this.product = product;
            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            _QueueName = rabbitMqConfig.Value.QueueName_UpdateProductName;
            _exchangeName = rabbitMqConfig.Value.Exchange_UpdateProductName;

            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password,
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout, true, false);

            _channel.QueueDeclare(_QueueName,
                        durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(_QueueName,_exchangeName,"");
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var body = Encoding.UTF8.GetString(args.Body.ToArray());
                var message = JsonConvert.DeserializeObject<UpdateProductNameDto>(body);

               

                var result = HandelMessage(message);
                if (result)
                {

                    _channel.BasicAck(args.DeliveryTag, false);
                }
                Console.WriteLine(body);
            };

            _channel.BasicConsume(_QueueName, false, consumer);

            return Task.CompletedTask;
        }

        private bool HandelMessage(UpdateProductNameDto update)
        {

            return product.UpdateProductName(Guid.Parse(update.ProductId), update.Name);

        }
    }

    public class UpdateProductNameDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string MessageId { get; set; }
        public DateTime createDate { get; set; }
    }
}

