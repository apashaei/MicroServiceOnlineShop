using Grpc.Core;
using Microservice.Web.FronEnd.Exceptions;
using Microservice.Web.FronEnd.Extensions;
using Microservice.Web.FronEnd.Services.ProductServices;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Microservice.Web.FronEnd.Services.HomePage
{
    public interface IHomePageServices
    {
        HomePageDataDto GetHomePageData();
    }

    public class HomePageServices : IHomePageServices
    {
        private readonly HttpClient client;

        public HomePageServices(HttpClient httpClient)
        {
            client = httpClient;
        }
        public HomePageDataDto GetHomePageData()
        {
            var response = client.GetAsync($"/api/homepage").Result;
            if (response.StatusCode==HttpStatusCode.InternalServerError )
            {
                throw new ServerErrorExceptioncs();
            }
            var homePageParts= response.ReadContentAs<List<HomePageGroupDto>>().Result;

            var responseMostSeller = client.GetAsync($"/api/Product/GetMostSellerProducts").Result;
            if (responseMostSeller.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ServerErrorExceptioncs();
            }
            var homePageMostsellers = responseMostSeller.ReadContentAs<List<MostSellerProductDto>>().Result;
            var homePageData = new HomePageDataDto
            {
                
                HomePageParts = homePageParts,
                MostSellerProductDtos = homePageMostsellers

            };
            return homePageData;

        }
    }
    public class Item
    {
        public int id { get; set; }
        public string imageSrc { get; set; }
        public string link { get; set; }
        public string title { get; set; }
        public string priority { get; set; }
    }

    public class HomePageGroupDto
    {
        public int part { get; set; }
        public List<Item> items { get; set; }
    }

    public class HomePageDataDto
    {
        public List<HomePageGroupDto> HomePageParts { get; set; }
        public List<MostSellerProductDto> MostSellerProductDtos { get; set; }
    }

    public class MostSellerProductDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int price { get; set; }

        public int? SellNumber { get; set; }
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
