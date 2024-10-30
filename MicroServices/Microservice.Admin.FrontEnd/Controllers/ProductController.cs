using Microservice.Admin.FrontEnd.Messaging.Configs;
using Microservice.Admin.FrontEnd.Messaging.SendMessage.ProductNameMessage;
using Microservice.Admin.FrontEnd.Models.ViewServices.Poduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microservice.Admin.FrontEnd.Models.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using Microservice.Admin.FrontEnd.Models.Dtos;

namespace Microservice.Admin.FrontEnd.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService servicecs;
        private string ExchangeName;

        public ProductController(IProductService servicecs, IOptions<RabbitMqConfigurations> rabbitMqConfig)
        {
            this.servicecs = servicecs;
            ExchangeName = rabbitMqConfig.Value.Exchange_UpdateProductName;
        }
        public IActionResult Index()

        {
            var data = servicecs.GetAllProducts();
            if (data.Count == 0)
            {
                ViewBag.Message = "محصولی یافت نشد";
            }
            return View(data);
        }
        public IActionResult AddNewProduct()
        {
            var result = servicecs.GetBrands();
            var categories = servicecs.GeDrpCategories();
            ViewBag.Brands = new SelectList(result.Data, "Id", "Name");
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
            return View();

        }

        [HttpPost]
        public IActionResult AddNewProduct(ProductDto productDto)
        {

            List<ProductFeaturesDto> Features  = new List<ProductFeaturesDto>();
            List<string> TableRows = new List<string>();
            List<IFormFile> Files = new List<IFormFile>();
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                ViewBag.Messages = allErrors.Select(p => p.ErrorMessage).ToList();
                return View();

            }

            if (!string.IsNullOrEmpty(productDto.TableData))
            {
                TableRows = JsonSerializer.Deserialize<List<string>>(productDto.TableData);
                foreach (var item in TableRows)
                {
                    var rowsItem = item.Split("__");
                    Features.Add(new ProductFeaturesDto
                    {
                        Group = rowsItem[0],
                        Key = rowsItem[1],
                        Value = rowsItem[2]
                    });
                };
                if (Features.Count == 0)
                {
                    ViewBag.Messages = new List<string>
                    {
                        "افزودن ویژگی الزامی می باشد."
                    };
                    return View();
                }

                // Process the table rows as needed
            }
            List<ImageDto> NewProductImage = new List<ImageDto>();

            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var files = Request.Form.Files[i];
                productDto.Images.Add(new ImageDto { Src = files.FileName });
                Files.Add(files);

            }

            List<string> ImageSourceList;

            

            productDto.ProductFeatures = Features;      
            var resultService = servicecs.AddNewProduct(productDto);
            ViewBag.Messages = resultService.Message;
            return View();

        }

        public IActionResult RemoveProduct(Guid Id)
        {
            var result = servicecs.RemoveProduct(Id);
            return RedirectToAction(nameof(Index));
        }


        [HttpPut]
        public IActionResult UpdateName(string Name, string ProductId, [FromServices] IMessageBus message)
        {
            ProductDto product = new ProductDto()
            {
                ProductCategoryId = Guid.Parse(ProductId),
                Name = Name,
                Description = Name,
                Id = Guid.Parse(ProductId),
                Images = new List<Models.Dtos.ImageDto> { },
                Price = 10000

            };
            servicecs.UpdateProduct(product);

            UpdateProductNameMessage updateProduct = new UpdateProductNameMessage()
            {
                Name = Name,
                ProductId = ProductId
            };
            message.SenMessage(updateProduct, ExchangeName);

            return RedirectToAction("Index");
        }
    }
}
