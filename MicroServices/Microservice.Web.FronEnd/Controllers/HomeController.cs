using Microservice.Web.FronEnd.Models;
using Microservice.Web.FronEnd.Services.HomePage;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Microservice.Web.FronEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomePageServices _homePageServices;
        public HomeController(ILogger<HomeController> logger, IHomePageServices homePageServices)
        {
            _logger = logger;
            _homePageServices = homePageServices;
        }

        public IActionResult Index()
        {
            var data = _homePageServices.GetHomePageData();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
