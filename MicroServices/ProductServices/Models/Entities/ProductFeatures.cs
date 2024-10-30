using ProductServices.Attributes;

namespace ProductServices.Models.Entities
{
    [Auditable]
    public class ProductFeatures
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
