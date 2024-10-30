using Microservice.Admin.FrontEnd.Extensions;

using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservice.Admin.FrontEnd.Models.ViewServices.HomePage
{
    public interface IHomePageServices
    {
        bool AddHomePageBanners(HomePageBannerDto bannerDto);
    }

    public class HomePageServices : IHomePageServices
    {
        private readonly HttpClient _httpClient;

        public HomePageServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public bool AddHomePageBanners(HomePageBannerDto bannerDto)
        {
            string serializeModel = System.Text.Json.JsonSerializer.Serialize(bannerDto);
            var content = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync("/api/homepage", content).Result;

            return response.ReadContentAs<bool>().Result;
        }
    }

    public class HomePageBannerDto
    {
        public int Id { get; set; }

        public PartEnum Part { get; set; }
        public string ImageSrc { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
    }

    public enum PartEnum
    {
        Part0 = 0,
        part1 = 1,
        part2 = 2,
        part3 = 3,
        part4 = 4,
    }
}
