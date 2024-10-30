namespace ProductServices.Models.Dtos
{
    public class ProductDtoTest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<ImageDto> Images { get; set; }
        public int Price { get; set; }
        public int AvailableStock { get; set; }

        public int RestockTreshold { get; set; }
        public int MaxStockTreshold { get; set; }
        public string? CategoryName { get; set; }
        public int? SellNumber { get; set; }
        public int Inventory { get; set; }

        public List<ProductFeaturesDto>? ProductFeatures { get; set; }
        public Guid BrandId { get; set; }
        public string? BrandName { get; set; }
        public Guid ProductCategoryId { get; set; }
    }
}
