using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime DatePay { get; set; }
        public string Authority { get; set; }
        public string RefId { get; set; }

        public Guid orderId { get; set; }
        public Order Order { get; set; }

    }
}
