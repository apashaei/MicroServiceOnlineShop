using ProductServices.Models.Dtos;
using ProductServices.Services.ProductServices;

namespace ProductServices.Decorator
{
    public class LoggingDecorator : ProductServiceDecorator
    {
        private readonly ILogger<LoggingDecorator> _logger;
        public LoggingDecorator(IProductServices productServices, ILogger<LoggingDecorator> logger) : base(productServices)
        {
            _logger = logger;
        }
        public Task<ResultDto> AddNewProduct(ProductDto product)
        {
            _logger.LogDebug("Add log before adding new product");
            return base.AddNewProduct(product);
        }
    }
}
