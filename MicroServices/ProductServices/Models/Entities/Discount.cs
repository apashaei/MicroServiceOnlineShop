using ProductServices.Attributes;

namespace ProductServices.Models.Entities
{
    [Auditable]
    public class Discount
    {
        public Guid Id { get; set; }
        public string DiscountCode {  get; set; }
        public int? DiscountAmount {  get; set; }
        public int? DisCountPercentage { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
