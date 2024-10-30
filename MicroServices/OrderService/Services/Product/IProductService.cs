
using OrderSevice.Infrastructure;
using OrderSevice.Models.Entities;

namespace OrderSevice.Services.Product
{
    public interface IProductService
    {
        OrderSevice.Models.Entities.Product GetProduct(ProductDto product);

        bool UpdateProductName(Guid productId, string productName);

    }

    public class ProductService : IProductService
    {
        private readonly DataBaseContext context;

        public ProductService(DataBaseContext context)
        {
            this.context = context;
        }
        public Models.Entities.Product GetProduct(ProductDto product)
        {
            var oldProduct = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (oldProduct != null)
            {
                return oldProduct;
            }
            return CreateProduct(product);
        }

        public bool UpdateProductName(Guid productId, string productName)
        {
            var product = context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                product.ProductName = productName;
                context.SaveChanges();
                return true;
            }
            else { return false; }
        }

        private Models.Entities.Product CreateProduct(ProductDto product)
        {
            var newProduct = new Models.Entities.Product()
            {
                ProductId = product.ProductId,
                Price = product.Price,
                ProductName = product.ProductName,

            };
            context.Add(newProduct);
            context.SaveChanges();
            return newProduct;
        }
    }


    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
    }
}

