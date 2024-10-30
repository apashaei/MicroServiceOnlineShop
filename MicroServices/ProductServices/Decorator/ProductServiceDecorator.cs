using ProductServices.MessingBus.RecieveMessage;
using ProductServices.Models.Dtos;
using ProductServices.Services.ProductServices;

namespace ProductServices.Decorator
{
    public abstract class ProductServiceDecorator : IProductServices
    {
        private readonly IProductServices _productServices;
        public ProductServiceDecorator(IProductServices productServices)
        {
            _productServices = productServices;
        }
        public Task<ResultDto> AddNewProduct(ProductDto product)
        {
           return _productServices.AddNewProduct(product);
        }

        public Task<PagenatedItemDto<GetAllProductsDto>> GetAllProducts(int Page = 0, int PageSize = 20)
        {
            return _productServices.GetAllProducts(Page, PageSize);
        }

        public Task<ResultDto<BrandListDto>> GetBrands()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto>> GetMostSellerProducts()
        {
            return _productServices.GetMostSellerProducts();
        }

        public Task<ResultDto<ProductDto>> GetProduct(Guid id)
        {
            return _productServices.GetProduct(id);
        }

        public Task<ResultDto<Guid>> RemoveProduct(Guid id)
        {
            return _productServices.RemoveProduct(id);
        }

        public Task<ResultDto> Updateproduct(UpdateProductDto product)
        {
            return _productServices.Updateproduct(product);
        }

        public Task UpdateProductsSellNumber(UpdateSellNumberMessafgeDto messageDto)
        {
            return _productServices.UpdateProductsSellNumber(messageDto);
        }

        Task<ResultDto<List<BrandListDto>>> IProductServices.GetBrands()
        {
            throw new NotImplementedException();
        }
    }
}
