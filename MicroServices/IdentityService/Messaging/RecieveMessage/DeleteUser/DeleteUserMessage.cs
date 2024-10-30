using IdentityService.Messaging.RecieveMessage.AddUser;
using IdentityService.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace IdentityService.Messaging.RecieveMessage.DeleteUser
{
    public class DeleteUserMessage : BackgroundService
    {
        private RabbitMQ.Client.IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly string _QueueName;
        private readonly string _exchangeName;
        private readonly IServiceScopeFactory serviceScopeFactory;


        public DeleteUserMessage(IOptions<RabbitMqConfiguration> rabbitMqConfig, IServiceScopeFactory serviceScopeFactory)
        {

            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            _QueueName = rabbitMqConfig.Value.QueueName_DeleteUser;
            this.serviceScopeFactory = serviceScopeFactory;

            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password,
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(_QueueName,
                        durable: true, exclusive: false, autoDelete: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var body = Encoding.UTF8.GetString(args.Body.ToArray());
                var message = JsonConvert.DeserializeObject<DeleteUserMessageDto>(body);



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

        private bool HandelMessage(DeleteUserMessageDto deleteUser)
        {
            using (var scope = serviceScopeFactory.CreateScope()) // Create a new scope
            {

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var user = userManager.FindByIdAsync(deleteUser.Id).Result;
                if (user != null)
                {
                    var result = userManager.DeleteAsync(user).Result;
                    if (result.Succeeded)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

        }
    }

    public class DeleteUserMessageDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
    }
}
