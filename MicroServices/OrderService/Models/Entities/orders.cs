namespace OrderSevice.Models.Entities
{
    public class Orders
    {
        public Guid Id { get; set; }
        public string UserId { get; private set; }
        public DateTime Orderplaced {  get; private set; }

        public bool Orderpaid { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }

        public Paymetnstatus Paymetnstatus { get; private set; }

        public int TotalPrice { get; set; }

        public virtual ICollection<Orderline> Orderlines { get; private set; }

        public Orders(string UserId, List<Orderline> orderlines,
            string FirstName,
            string LastName,
            string Address,
            string PhoneNumber
            )
        {
            this.UserId = UserId;
            this.Orderlines = orderlines;
            Orderpaid = false;
            Orderplaced = DateTime.Now;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.PhoneNumber = PhoneNumber;
            this.Paymetnstatus = Paymetnstatus.NotPaid;
        }

       
        public Orders()
        {
            
        }

        public void RequestPay()
        {
            Paymetnstatus = Paymetnstatus.RequestPay;
        }
        public void PayDone()
        {
            Paymetnstatus = Paymetnstatus.Ispaied;
        }
    }

    public enum Paymetnstatus
    {
        NotPaid=0,
        RequestPay=1,
        Ispaied=2
    }

    public class Orderline
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Orders orders { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }

}
