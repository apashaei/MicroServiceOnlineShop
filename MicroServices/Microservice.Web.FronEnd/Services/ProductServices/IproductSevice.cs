using Microservice.Web.FronEnd.Models.NewFolder;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Text.Json;

namespace Microservice.Web.FronEnd.Services.ProductServices
{
    public interface IproductSevice
    {
        List<ProductDto> GetAllProducts();
        ProductDto GetProductById(Guid id);
    }

    public class productSevice : IproductSevice
    {
        private readonly HttpClient _httpClient;
        public productSevice(HttpClient client)
        {
            _httpClient = client;
        }
        public List<ProductDto> GetAllProducts()
        {


            var response = _httpClient.GetAsync("/api/Product").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductDto>>(json);
            return products;
        }

        public ProductDto GetProductById(Guid id)
        {
            var response = _httpClient.GetAsync($"/api/Product/{id}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var product = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductDto>(json);
            return product;

        }
    }

    public class Category
    {
   
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class ProductDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int price { get; set; }
        public string categoryId { get; set; }
        public Category category { get; set; }
    }
}
