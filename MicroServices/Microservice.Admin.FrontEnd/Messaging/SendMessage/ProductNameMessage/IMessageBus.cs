using Microservice.Admin.FrontEnd.Messaging.Configs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using RabbitMQ.Client;
using System.Text;
using IConnection = RabbitMQ.Client.IConnection;

namespace Microservice.Admin.FrontEnd.Messaging.SendMessage.ProductNameMessage
{
    public interface IMessageBus
    {
        void SenMessage(BaseMessage message, string ExchangeName);
    }

    public class RabbitMQMessageBus : IMessageBus
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;
        private readonly RabbitMqConfigurations _rabbitMqConfig;

        public RabbitMQMessageBus(IOptions<RabbitMqConfigurations> rabbitMqConfig)
        {
            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            CreateRabbitMQConnection();
        }
        public void SenMessage(BaseMessage message, string ExchangeName)
        {
            if (CheckRabbitMqConnection())
            {
                using (var channel = _connection.CreateModel())
                {

                    channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout,true, false, null);

                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.BasicPublish(exchange: ExchangeName,
                        routingKey: "", basicProperties: properties, body);
                }
            }
        }

        private void CreateRabbitMQConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (System.Exception ex)
            {
                {
                    throw new Exception();

                }
            }
        }

        private bool CheckRabbitMqConnection()
        {
            if (_connection != null)
            {
                return true;
            }
            else
            {
                CreateRabbitMQConnection();
                return _connection != null;
            }
        }
    }

    public class BaseMessage
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public DateTime createDate { get; set; } = DateTime.Now;
    }
}
