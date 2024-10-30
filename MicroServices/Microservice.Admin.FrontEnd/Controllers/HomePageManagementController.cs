using Microservice.Admin.FrontEnd.Models.ViewModels;
using Microservice.Admin.FrontEnd.Models.ViewServices.HomePage;
using Microservice.Admin.FrontEnd.Models.ViewServices.StaticFile;
using Microservice.Admin.FrontEnd.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Microservice.Admin.FrontEnd.Controllers
{

    //[Authorize(Roles ="Admin")]
    public class HomePageManagementController : Controller
    {
        private readonly IHomePageServices services;
        private readonly IStaticFileServices staticFileServices;
        ReadImageSrc imageSrc;
        public HomePageManagementController(IHomePageServices services,IStaticFileServices staticFileServices)
        {
            imageSrc = new ReadImageSrc();
            this.services = services;
            this.staticFileServices = staticFileServices;   
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Index(AddHomePageViewModel pageViewModel)
        {
            
            List<string> Messages = new List<string>();
            if(!ModelState.IsValid)
            {
               return View(pageViewModel);
            }

            var res = staticFileServices.UploadImagesAsync(pageViewModel.ImageFile);
            var homePageDto = new HomePageBannerDto
            {
                Id = pageViewModel.Id,
                ImageSrc = res.fileNameAddress.FirstOrDefault(),
                Link = pageViewModel.Link,
                Part = pageViewModel.Part,
                Priority = pageViewModel.Priority,
                Title = pageViewModel.Title,
            };
            var result = services.AddHomePageBanners(homePageDto);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else return View(pageViewModel);
        }
    }
}
