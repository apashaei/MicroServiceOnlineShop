
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderSevice.Infrastructure;
using OrderSevice.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderSevice.MessagingBus.RecievedMessage
{
    public class PayDoneInfoMessagecs : BackgroundService
    {
        private RabbitMQ.Client.IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly string _QueueName;
        private readonly IOrderServices orderServices;
        private readonly DataBaseContext context;

        public PayDoneInfoMessagecs(IOptions<RabbotMQConfig> rabbitMqConfig, IOrderServices orderServices,DataBaseContext context)
        {
            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            _QueueName = rabbitMqConfig.Value.QueueName_SendPayinfo;

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
            this.context = context;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var body = Encoding.UTF8.GetString(args.Body.ToArray());
                var message = JsonConvert.DeserializeObject<PaymentorderMessage>(body);

                var result = HandelMessage(message);
                if (result)
                {

                    _channel.BasicAck(args.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(_QueueName, false, consumer);

            return Task.CompletedTask;
        }

        private bool HandelMessage(PaymentorderMessage message)
        {

            var order =  context.Orders.FirstOrDefault(p=>p.Id == message.OrderId);
            order.PayDone();
            context.SaveChanges();
            return true;

        }
    }
    public class PaymentorderMessage
    {
        public Guid OrderId { get; set; }
    }

}
