using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ProductServices.MessingBus.SendMessage
{
    public interface IMessageBus
    {
        public void SendMessage(BaseMessage message, string queueName);
    }

    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;
        private readonly RabbitMqConfiguration _rabbitMqConfig;

        public RabbitMqMessageBus(IOptions<RabbitMqConfiguration> rabbitMqConfig)
        {
            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            CreateRabbitMQConnection();
        }
        public void SendMessage(BaseMessage message, string exchangeName)
        {
            if (CheckRabbitMqConnection())
            {
                using (var channel = _connection.CreateModel())
                {

                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true, false, null);

                    var json = JsonConvert.SerializeObject(message);

                    var body = Encoding.UTF8.GetBytes(json);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.BasicPublish(exchange: exchangeName,
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
}
