namespace Microservice.Admin.FrontEnd.Models.Dtos.ProductDtos
{
    public class GetAllProductsDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<ProductItemDto> Items { get; set; }
        public PagerDto Pager { get; set; }
        public bool HasPrevPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
