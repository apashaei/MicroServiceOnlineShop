using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messaging.Configs
{
    public class RabbitMQConfig
    {
        public string Hostname { get; set; }

        
        public string QueueName_SendOrderToPayment { get; set; }
        public string QueueName_SendPayinfo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
