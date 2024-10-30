using IdentityService.Models;
using IdentityService.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace IdentityService.Messaging.RecieveMessage.UpdateUser
{
    public class UpdateUserinfo:BackgroundService
    {
        private RabbitMQ.Client.IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly string _QueueName;
        private readonly string _exchangeName;

        private readonly IServiceScopeFactory serviceScopeFactory;

        public UpdateUserinfo(IOptions<RabbitMqConfiguration> rabbitMqConfig, IServiceScopeFactory serviceScopeFactory)
        {
            

            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            _QueueName = rabbitMqConfig.Value.QueueName_EditUser;
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
                var message = JsonConvert.DeserializeObject<UpdateUserInfoDto>(body);



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

        private bool HandelMessage(UpdateUserInfoDto updateUser)
        {
            using (var scope = serviceScopeFactory.CreateScope()) // Create a new scope
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var user = userManager.FindByNameAsync(updateUser.Username.ToString()).Result;
                var role = roleManager.FindByNameAsync(updateUser.RoleName).Result;

                user.FullName = updateUser.FullName;
                user.UserName = updateUser.Username;
                user.PhoneNumber = updateUser.phoneNumber;
                user.Email = updateUser.Email;
                var result = userManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                {

                    var roleResult = userManager.AddToRoleAsync(user, updateUser.RoleName).Result;
                    if (!roleResult.Succeeded)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
                
        }
    }

    public class UpdateUserInfoDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string phoneNumber { get; set; }
        public string RoleName { get; set; }
    }
}
