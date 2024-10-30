﻿namespace BasketServices.MessagingBus
{
    public class RabbitMqConfiguration
    {
        public string Hostname { get; set; }

        public string QueueName_Ordercreated { get; set; }
        public string QueueName_UpdateProductName { get; set; }
        public string Exchange_UpdateProductName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
