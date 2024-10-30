using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductServices.Exceptions;
using ProductServices.MessingBus;
using ProductServices.MessingBus.RecieveMessage;
using ProductServices.MessingBus.SendMessage;
using ProductServices.Models.Context;
using ProductServices.Models.Dtos;
using ProductServices.Models.Entities;
using ProductServices.Services.CategoryServices;
using ProductServices.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace ProductServices.Services.ProductServices
{
    public interface IProductServices
    {
        Task<ResultDto> AddNewProduct(ProductDto product);
        Task<PagenatedItemDto<GetAllProductsDto>> GetAllProducts(int Page=0, int PageSize=20);
        Task<ResultDto<ProductDto>> GetProduct(Guid id);
        Task<ResultDto> Updateproduct(UpdateProductDto product);
        Task UpdateProductsSellNumber(UpdateSellNumberMessafgeDto messageDto);
        Task<List<ProductDto>> GetMostSellerProducts();
        Task<ResultDto<Guid>> RemoveProduct(Guid id);
        Task<ResultDto<List<BrandListDto>>> GetBrands();
    }

    public class ProductService : IProductServices
    {
        private readonly DataBaseContext dataBaseContext;
        private readonly IMessageBus messageBus;
        private readonly ILogger<ProductService> _logger;
        
        private string ExchangeName;

        public ProductService(DataBaseContext dataBaseContext, IMessageBus messageBus, IOptions<RabbitMqConfiguration> options, ILogger<ProductService> logger)
        {
            this.dataBaseContext = dataBaseContext;
            this.messageBus = messageBus;
            ExchangeName = options.Value.ExchangeName_UpdatePrduct;
            _logger = logger;
            
        }
        public async Task<ResultDto> AddNewProduct(ProductDto product)
        {
            var brand = dataBaseContext.Brands.FirstOrDefault(p=>p.Id==product.BrandId);

          
            var newProduct = new Product()
            {
               Name = product.Name,
               Price = product.Price,
               Inventory = product.Inventory,
               MaxStockTreshold = product.MaxStockTreshold,
               RestockTreshold = product.RestockTreshold,
               Description = product.Description,
               AvailableStock = product.AvailableStock,
               Brand= brand,
               ProductCategoryId = product.ProductCategoryId,
            };
            dataBaseContext.Add(newProduct);
            List<Images> images = new List<Images>();
            List<ProductFeatures> features = new List<ProductFeatures>();   
            foreach(var image in product.Images)
            {
                images.Add(new Images
                {
                    Src = image.Src,
                    ProductId = newProduct.Id,
                });
            };
            dataBaseContext.Images.AddRange(images);

            foreach (var feature in product.ProductFeatures)
            {
                features.Add(new ProductFeatures
                {
                    Description = feature.Group,
                    ProductId = newProduct.Id,
                    Key = feature.Key
                });
            }
            dataBaseContext.ProductFeatures.AddRange(features);
            var result = dataBaseContext.SaveChanges();
            if (result > 0)
            {
                return new ResultDto
                {
                    Success = true,
                    Message = ""
                };
            }
            else
            {
                return new ResultDto
                {
                    Success = false,
                    Message = "خطایی رخ داده است."
                };
            }

            
        }

        public async Task<PagenatedItemDto<GetAllProductsDto>> GetAllProducts(int Page = 0, int PageSize = 20)
        {
                     
            int rowCount = 0;
            var products = dataBaseContext.Products.Select(p=> new GetAllProductsDto
            {
                Name = p.Name,
                AvailableStock= p.AvailableStock,
                Description = p.Description,
                Price = p.Price,
                Id = p.Id,
            }).ToPaged(Page,PageSize, out rowCount).ToList();

       
         
            return new PagenatedItemDto<GetAllProductsDto>(Page,PageSize,rowCount,products);

        }

        public async Task<ResultDto<List<BrandListDto>>> GetBrands()
        {
            var brands = dataBaseContext?.Brands.Select(p=> new BrandListDto
            {
                Id = p.Id.ToString(),
                Name = p.Name,  
            }).ToList();
            return new ResultDto<List<BrandListDto>>
            {
                Data = brands,
                Success = true,
                Message = ""
            };
        }

        public async Task<List<ProductDto>> GetMostSellerProducts()
        {
            var result = await dataBaseContext.Products
                .Include(P=>P.Images).OrderBy(p=>p.SellNumber).Take(100).Select(p=> new ProductDto
            {
                Id=p.Id,
                Name = p.Name,
                Images = new List<ImageDto>
                {
                    new ImageDto
                    {
                        
                        Src = p.Images.FirstOrDefault().Src
                    }
                },
                Price = p.Price,
            }).ToListAsync();

            

            return result;
        }

        public async Task<ResultDto<ProductDto>> GetProduct(Guid id)
        {
            var product = dataBaseContext.Products.AsNoTracking()
                .Include(p => p.Images)
                .Include(p => p.ProductCategory)
                .Include(p => p.Brand)
                .Include(p => p.Discount)
                .Select(p => new ProductDto
                {
                    AvailableStock = p.AvailableStock,
                    Description = p.Description,
                    Price = p.Price,
                    Id = id,
                    BrandName = p.Brand.Name,
                    CategoryName = p.ProductCategory.Name,
                    Name = p.Name,
                    //ProductFeatures = p.ProductFeatures.Select(p => new ProductFeaturesDto
                    //{
                    //    Description = p.Description,
                    //    Id = p.Id,
                    //    Key = p.Key,
                    //}).ToList(),
                    Inventory = p.Inventory,
                    SellNumber = p.SellNumber,
                    RestockTreshold = p.RestockTreshold,
                    MaxStockTreshold = p.MaxStockTreshold,
                    Discount = p.Discount != null ? new DiscountDto
                    {
                        Id = p.Discount.Id,
                        DiscountCode = p.Discount.DiscountCode,
                        DiscountAmount = p.Discount.DiscountAmount != null ? p.Discount.DiscountAmount : p.Discount.DisCountPercentage,
                    } : null,
                    Images = p.Images != null ? p.Images.Select(image => new ImageDto
                    {
                        Src = image.Src,
                        Id = image.Id,
                    }).ToList() : null

                }).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return new ResultDto<ProductDto>
                {

                    Message = "خطایی رخ داده است.",
                    Success = false,
                };
            }
            return new ResultDto<ProductDto>
            {
                Data = product,
                Message="",
                Success = true,
            };
        }

        public async Task<ResultDto<Guid>> RemoveProduct(Guid id)
        {
            var product = dataBaseContext.Products.FirstOrDefault(p => p.Id == id); 
            if (product == null)
            {

                return new ResultDto<Guid>
                {
                    Success = false,
                    Message = "محصول یافت نشد."
                };
            }
            dataBaseContext.Entry(product).Property("IsRemoved").CurrentValue = true;
            dataBaseContext.Entry(product).Property("RemoveTime").CurrentValue=DateTime.Now;
            await dataBaseContext.SaveChangesAsync();
            return new ResultDto<Guid>
            {
                Data = id,
                Success = true,
                Message = "حدف با موفقیت انجام شد",
            };
        }

        public async Task<ResultDto> Updateproduct(UpdateProductDto product)
        {
            var productResult = dataBaseContext.Products.FirstOrDefault(p=>p.Id == product.Id);
            var brand = dataBaseContext.Brands.FirstOrDefault(p => p.Id == product.BrandId);
            //var category = dataBaseContext.Categories.FirstOrDefault(p => p.Id == product.ProductCategoryId);
            if(productResult != null)
            {
               productResult.SellNumber = product.SellNumber;
                productResult.Brand = brand;
                productResult.Price = product.Price;
                productResult.Description = product.Description;
                //productResult.ProductCategory = category;
                productResult.AvailableStock  = product.AvailableStock; 
                productResult.RestockTreshold = product.RestockTreshold;
                productResult.Name = product.Name;
            };
            if (productResult == null)
            {
                return new ResultDto
                {
                    Success = false,
                    Message = "محصول مورد نظر یافت نشد."
                };
            }
            //List<ProductFeatures> features = new List<ProductFeatures>();
            //foreach (var feature in product.ProductFeatures)
            //{
            //    features.Add(new ProductFeatures
            //    {
            //        Description = feature.Description,
            //        Key = feature.Key,
                    
            //    });
            //};
            //dataBaseContext.ProductFeatures.AddRange(features);
            var result = dataBaseContext.SaveChanges();
            if (result > 0)
            {
                var message = new UpdateProductMessageDto
                {
                    ProductId = product.Id.ToString(),
                    Name = product.Name,
                };
                messageBus.SendMessage(message, ExchangeName);
                return new ResultDto
                {
                    Success = true,
                    Message = "به روز رسانی با موفقیت انجام شد."
                };
            }
            return new ResultDto
            {
                Success = false,
                Message = "خطایی رخ داده است."
            };
        }

        public async Task UpdateProductsSellNumber(UpdateSellNumberMessafgeDto messageDto)
        {
            
                await  dataBaseContext.Products
                    .Where(p => messageDto.ProductIds.Contains(p.Id))  
                    .ExecuteUpdateAsync(p => p.SetProperty(x => x.SellNumber, x => x.SellNumber+1));   
        }
    }
}
