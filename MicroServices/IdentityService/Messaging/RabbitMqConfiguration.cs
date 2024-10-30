namespace IdentityService.Messaging
{
    public class RabbitMqConfiguration
    {
        public string Hostname { get; set; }

        public string QueueName_EditUser { get; set; }
        public string QueueName_AddUser { get; set; }
        public string QueueName_DeleteUser { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
