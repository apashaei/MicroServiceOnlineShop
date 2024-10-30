using Microservice.Admin.FrontEnd.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Text.Json;

using Microservice.Admin.FrontEnd.Extensions;
using System.Text;
using Newtonsoft.Json;
using Microservice.Admin.FrontEnd.Models.Dtos.ProductDtos;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microservice.Admin.FrontEnd.Exceptions;

namespace Microservice.Admin.FrontEnd.Models.ViewServices.Poduct
{
    public interface IProductService
    {
        ResultDto RemoveProduct(Guid productId);
        GetAllProductsDto GetAllProducts();
        bool UpdateProduct(ProductDto product);
        ResultDto AddNewProduct(ProductDto product);
        ResultDto<List<BrandListDto>> GetBrands();
        ResultDto<List<CategoriesDto>> GeDrpCategories();

        ProductDto GetProduct(Guid id);
    }

    public class ProductService : IProductService
    {
        //private readonly RestClientOptions restClientOptions;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient,IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
  
            _httpClient = httpClient;
        }

        public ResultDto AddNewProduct(ProductDto product)
        {
        
            string serializeModel = System.Text.Json.JsonSerializer.Serialize(product);
            var content = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync("/api/product", content).Result;

            return response.ReadContentAs<ResultDto>().Result;
        }

        public ResultDto<List<CategoriesDto>> GeDrpCategories()
        {
            var response = _httpClient.GetAsync($"/api/Category/GeDrpCategories").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var categories = JsonConvert.DeserializeObject<ResultDto<List<CategoriesDto>>>(json);
            return categories;
        }

        public GetAllProductsDto GetAllProducts()
        {

            var token = httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
            var response = _httpClient.GetAsync($"/api/Product").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode ==System.Net.HttpStatusCode.NotFound)
            {
                throw new InternalServerError();
            }
            var products = JsonConvert.DeserializeObject<GetAllProductsDto>(json);
            return products;
        }

        public ResultDto<List<BrandListDto>> GetBrands()
        {
            var response = _httpClient.GetAsync($"/api/Product/GetBrands").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var brands = JsonConvert.DeserializeObject<ResultDto<List<BrandListDto>>>(json);
            return brands;
        }

        public ProductDto GetProduct(Guid id)
        {

            var response = _httpClient.GetAsync($"/api/Product").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var products = JsonConvert.DeserializeObject<ProductDto>(json);
            return products;
        }

        public ResultDto RemoveProduct(Guid productId)
        {
            var response = _httpClient.DeleteAsync($"/api/Product/{productId}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var products = JsonConvert.DeserializeObject<ResultDto>(json);
            return products;
        }

        public bool UpdateProduct(ProductDto product)
        {

            string serializeModel = System.Text.Json.JsonSerializer.Serialize(product);
            var content = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PutAsync("/api/Product", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        private ResultDto ReturnResult(RestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "",
                };

            }
            else
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "",
                };
            }
        }
    }




}

