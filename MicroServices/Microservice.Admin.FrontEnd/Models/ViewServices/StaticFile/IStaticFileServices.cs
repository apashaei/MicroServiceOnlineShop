using Microservice.Admin.FrontEnd.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Microservice.Admin.FrontEnd.Models.ViewServices.StaticFile
{
    public interface IStaticFileServices
    {
        UploadFileDto UploadImagesAsync(IFormFile formFile);
    }

    public class StaticFileServices : IStaticFileServices
    {
        private readonly HttpClient _httpClient;


        public StaticFileServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public UploadFileDto UploadImagesAsync(IFormFile formFile)
        {
            using var formData = new MultipartFormDataContent();

            var streamContent = new StreamContent(formFile.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);

            formData.Add(streamContent, "files", formFile.FileName);
            var response = _httpClient.PostAsync($"api/Images/mysecretkey", formData).Result;
            var result = response.ReadContentAs<UploadFileDto>();
            return result.Result;
        }
    }

    public class UploadFileDto
    {
        public bool status { get; set; }
        public List<string> fileNameAddress { get; set; }
    }
}
