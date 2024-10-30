namespace ProductServices.Models.Dtos
{
    public class DiscountDto
    {
        public Guid Id { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountAmount { get; set; }
        public int? DisCountPercentage { get; set; }
    }
}
