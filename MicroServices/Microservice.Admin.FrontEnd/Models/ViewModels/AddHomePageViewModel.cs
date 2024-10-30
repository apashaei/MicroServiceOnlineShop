using Microservice.Admin.FrontEnd.Models.ViewServices.HomePage;
using System.ComponentModel.DataAnnotations;

namespace Microservice.Admin.FrontEnd.Models.ViewModels
{
    public class AddHomePageViewModel
    {

        public int Id { get; set; }

        public PartEnum Part { get; set; }

        [Required(ErrorMessage = "ادرس تصویر الزامی می باشد.")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "لینک تصویر الزامی می باشد.")]
        public string Link { get; set; }

        [Required(ErrorMessage = "عنوان تکراری می باشد.")]
        public string Title { get; set; }
        public string Priority { get; set; }
    }
}
