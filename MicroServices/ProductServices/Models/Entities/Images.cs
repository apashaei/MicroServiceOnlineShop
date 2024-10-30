using ProductServices.Attributes;

namespace ProductServices.Models.Entities
{

    [Auditable]
    public class Images
    {
        public Guid Id { get; set; }
        public string Src { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
    }
}
