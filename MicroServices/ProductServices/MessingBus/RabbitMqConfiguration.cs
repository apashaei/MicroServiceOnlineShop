﻿namespace ProductServices.MessingBus
{
    public class RabbitMqConfiguration
    {
        public string Hostname { get; set; }
        public string ExchangeName_UpdatePrduct { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
