namespace BasketServices.Models.Entities
{
    public class BasketItems
    {
        public Guid Id { get; set; }       
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }
        
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public void SetQuantity(int quantity)
        {

            this.Quantity = quantity;

        }
    }
    }
