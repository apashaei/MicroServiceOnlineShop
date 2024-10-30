namespace OrderSevice.Models.Dtos
{
    public class discountDto
    {
      
            public Guid Id { get; set; }
            public string Code { get; set; }
            public int Amount { get; set; }

            public bool Used { get; set; }
        
    }
}
