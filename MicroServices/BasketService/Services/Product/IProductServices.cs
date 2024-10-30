using BasketServices.Infrastructure;

namespace BasketServices.Services.Product
{
    public interface IProductServices
    {
        bool UpdateProductName(Guid ProductId, string productName);
    }

    public class ProductServices : IProductServices
    {
        private readonly DataBaseContext _context;
        public ProductServices(DataBaseContext context)
        {
            _context = context;
        }
        public bool UpdateProductName(Guid ProductId, string productName)
        {
            var product = _context.Products.FirstOrDefault(p=>p.Id == ProductId);
            if (product != null)
            {
                product.ProductName = productName;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
