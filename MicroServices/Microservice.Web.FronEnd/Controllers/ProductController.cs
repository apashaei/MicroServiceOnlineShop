using Microservice.Web.FronEnd.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Microservice.Web.FronEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly IproductSevice iproductSevice;

        public ProductController(IproductSevice iproductSevice)
        {
            this.iproductSevice = iproductSevice;
        }
        public IActionResult Index()
        {
            var products = iproductSevice.GetAllProducts();
            return View(products);
        }
        public IActionResult Details(Guid id)
        {
            var product = iproductSevice.GetProductById(id);
            return View(product);
        }
    }
}
