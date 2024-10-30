using ProductServices.Attributes;

namespace ProductServices.Models.Entities
{
    [Auditable]
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int AvailableStock { get; set; }

        public int RestockTreshold { get; set; }
        public int MaxStockTreshold { get; set; }

        public int? SellNumber { get; set; }
        public int Inventory {  get; set; }
        public ICollection<Images> Images { get; set; }
        public Brand Brand { get; set; }
        public Guid BrandId { get; set; }

        public ICollection<ProductFeatures> ProductFeatures { get; set; }
        public CategoryComponent ProductCategory { get; set; }
        public Guid ProductCategoryId { get; set; }

        public Discount Discount { get; set; }
        public Guid? DiscountId { get; set; }
    }
    }
