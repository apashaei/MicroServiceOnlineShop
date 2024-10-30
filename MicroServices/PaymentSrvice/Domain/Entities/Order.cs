using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
