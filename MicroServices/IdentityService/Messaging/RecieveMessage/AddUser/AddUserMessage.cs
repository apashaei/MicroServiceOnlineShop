using IdentityService.Messaging.RecieveMessage.UpdateUser;
using IdentityService.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace IdentityService.Messaging.RecieveMessage.AddUser
{
    public class AddUserMessage:BackgroundService
    {

        private RabbitMQ.Client.IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly string _QueueName;
        private readonly string _exchangeName;

        private readonly IServiceScopeFactory serviceScopeFactory;
        public AddUserMessage(IOptions<RabbitMqConfiguration> rabbitMqConfig, IServiceScopeFactory serviceScopeFactory)
        {

            _hostname = rabbitMqConfig.Value.Hostname;
            _username = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            _QueueName = rabbitMqConfig.Value.QueueName_AddUser;
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
                var message = JsonConvert.DeserializeObject<AddUserMessageDto>(body);



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

        private bool HandelMessage(AddUserMessageDto addUser)
        {
            using (var scope = serviceScopeFactory.CreateScope()) // Create a new scope
            {
                var newUser = new User
                {
                    FullName = addUser.FullName,
                    Email = addUser.Email,
                    UserName = addUser.UserName,
                    PhoneNumber = addUser.PhoneNumber,
                    Id = addUser.Id,

                };
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                var createdUser = userManager.CreateAsync(newUser, addUser.Password).Result;
                if (roleManager.RoleExistsAsync(addUser.Role.Name).Result)
                {

                    var addedRole = userManager.AddToRoleAsync(newUser, addUser.Role.Name).Result;
                    if (createdUser.Succeeded && addedRole.Succeeded)
                    {
                     

                        return true;
                    }
                }

                else
                {
                    var role = roleManager.CreateAsync(new IdentityRole
                    {
                        Name = addUser.Role.Name,
                    }).Result;
                    var addedRole = userManager.AddToRoleAsync(newUser, addUser.Role.Name).Result;
                    if (createdUser.Succeeded && addedRole.Succeeded)
                    {
                    
                        return true;
                    }
                }
                return false;
            }

        }
    }

    public class AddUserMessageDto
    {
        public string? Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public UserRoleMessage Role { get; set; }
        public string Password { get; set; }
    }
    public class UserRoleMessage 
    {
        public string Name { get; set; }
        public string? Id { get; set; }
    }
}
