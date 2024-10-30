namespace ProductServices.Models.Dtos
{
    public class GetAllProductsDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int AvailableStock { get; set; }
    }
}
