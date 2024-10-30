using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messaging.SendMessage.SendPaDoneMessage
{
    public class PayDoneMessageDto:BaseMessage
    {
        public Guid OrderId { get; set; } 
        public bool PayDone { get; set; }
    }
}
