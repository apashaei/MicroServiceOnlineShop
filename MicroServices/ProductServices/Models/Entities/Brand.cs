using ProductServices.Attributes;

namespace ProductServices.Models.Entities
{
    [Auditable]
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
