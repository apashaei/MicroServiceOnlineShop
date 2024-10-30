using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace OrderSevice.MessagingBus.SendMessage
{
    public interface IMassageBus
    {
        void SendMassage(BaseMessage message, String QueueName);
    }

    public class RabbitMQSendMessage : IMassageBus
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;
        

        public RabbitMQSendMessage(IOptions<RabbotMQConfig> rabbitMqConfig)
        {
            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            
        }
        public void SendMassage(BaseMessage message, string QueueName)
        {
            if (CheckRabbitMqConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName,
                        durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.BasicPublish(exchange: "",
                        routingKey: QueueName, basicProperties: properties, body);
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

    public class RedisSendMessage : IMassageBus
    {
        private readonly ConnectionMultiplexer _connection;

        public RedisSendMessage(IOptions<RedisConfig> options)
        {
             _connection = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
        }
        public async void SendMassage(BaseMessage message, string channelName)
        {
            var subscriber = _connection.GetSubscriber();
            var jsonDats = JsonConvert.SerializeObject(message);
            await subscriber.PublishAsync(channelName, jsonDats);
        }  
    }

    public class BaseMessage
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public DateTime CreateMessage { get; set; } = DateTime.Now;
    }
}
