namespace ProductServices.Models.Dtos
{
    public class GetProductsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Inventory {  get; set; }
        public int SellerNumber { get; set; }
    }
}
