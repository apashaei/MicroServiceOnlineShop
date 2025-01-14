﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;

namespace BasketServices.MessagingBus
{
    public interface IMassageBus
    {
        void SendMassage(BaseMessage message, string queueName);
    }

    public class RabbitMQMessageBus : IMassageBus
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;
        private readonly RabbitMqConfiguration _rabbitMqConfig;

        public RabbitMQMessageBus(IOptions<RabbitMqConfiguration> rabbitMqConfig)
        {
            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
        }
        public void SendMassage(BaseMessage message, string queueName)
        {
            if (CheckRabbitMqConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queueName,
                        durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.BasicPublish(exchange: "",
                        routingKey: queueName, basicProperties: properties, body);
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
            public DateTime CreationTime { get; set; } = DateTime.Now;
        }
    
}
