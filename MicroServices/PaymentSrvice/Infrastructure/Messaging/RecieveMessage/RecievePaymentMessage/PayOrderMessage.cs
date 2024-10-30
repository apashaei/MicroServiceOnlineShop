using Application.PaymentSRVC;
using Infrastructure.Messaging.Configs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messaging.RecieveMessage.RecievePaymentMessage
{
    public class PayOrderMessage : BackgroundService
    {

        private RabbitMQ.Client.IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly string _QueueName;
        private readonly IPaymentServices _paymentServices;

        public PayOrderMessage(IOptions<RabbitMQConfig> rabbitMqConfig,IPaymentServices payment)
        {
            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            _QueueName = rabbitMqConfig.Value.QueueName_SendOrderToPayment;
            _paymentServices = payment;

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
    
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var body = Encoding.UTF8.GetString(args.Body.ToArray());
                var order = JsonConvert.DeserializeObject<OrderPayment>(body);

                var result = HandelMessage(order);
                if (result)
                {

                    _channel.BasicAck(args.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(_QueueName, false, consumer);

            return Task.CompletedTask;
        }

        private bool HandelMessage(OrderPayment order)
        {

            return _paymentServices.CreatePayment(order.OrderId,order.Amount);

        }

        public class OrderPayment
        {
            public Guid OrderId { get; set; }
            public int Amount { get; set; }
        }
    }
}
