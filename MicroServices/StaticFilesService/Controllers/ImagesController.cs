using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaticFilesService.Model.Dtos;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace StaticFilesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IHostingEnvironment _environment;

        public ImagesController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost("{apiKey}")]
        public IActionResult Post(string apiKey, IFormFileCollection files)

        {
            if (apiKey != "mysecretkey")
            {
                return BadRequest();
            }
            try
            {
             
                var folderName = Path.Combine("Resource", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (files != null)
                {
                    return Ok(UploadFile(files));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internet server error");
                throw new Exception("Upload image error", ex);
            };
        }

        private UploadDto UploadFile(IFormFileCollection files)
        {
            string newName = Guid.NewGuid().ToString();
            var date = DateTime.Now;
            string folder = $@"Resources\images\{date.Year}-{date.Month}\";
            var uploadsRootFolder = Path.Combine(_environment.ContentRootPath, $@"wwwroot\{folder}");
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }
            List<string> adress = new List<string>();
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = newName + file.FileName;
                    var filePath = Path.Combine(uploadsRootFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    adress.Add(folder + fileName);
                }
            }
            return new UploadDto()
            {
                FileNameAddress = adress,
                Status = true,
            };
        }
    }
}
