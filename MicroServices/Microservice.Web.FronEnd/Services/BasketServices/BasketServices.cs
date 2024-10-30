using Microservice.Web.FronEnd.Models.NewFolder;
using Microservice.Web.FronEnd.Services.DiscountServices;
using Newtonsoft.Json;
using NuGet.Configuration;
using RestSharp;
using System.Text;
using System.Text.Json;

namespace Microservice.Web.FronEnd.Services.BasketServices
{
    public class BasketServices : IBasketServices
    {
        private readonly RestClientOptions restClientOptions;
        private readonly HttpClient client;

        public BasketServices( HttpClient client)
        {
            
        
            this.client = client;
        }

        public ResultDto AddItemToBasket(AddItemToBasketDto addItem)
        {
            string serializeModel = System.Text.Json.JsonSerializer.Serialize(addItem);
            var content = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/Basket", content).Result;
            return ReturnResult(response);
        }

        public ResultDto ApplyBasketOnDiscount(string BasketId, string DiscountId)
        {
            var response = client.PutAsync($"/api/Basket/{BasketId}/{DiscountId}",null).Result;
            return ReturnResult(response);
        }

        public ResultDto CheckoutBasket(CheckoutDto checkout)
        {

            string serializeModel = System.Text.Json.JsonSerializer.Serialize(checkout);
            var content = new StringContent(serializeModel, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync($"/api/Basket/checkoutBasket", content).Result;
            return ReturnResult(response);
        }

        public BasketDto GetBasket(string UserId)
        {
            var response = client.GetAsync($"/api/Basket?UserId={UserId}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var basket = JsonConvert.DeserializeObject<BasketDto>(json);
            return basket;
        }

        public string GetOrCreateBasket(string UserId)
        {
            var response = client.GetAsync($"/api/Basket/GetOrCreate?UserId={UserId}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var basket = JsonConvert.DeserializeObject<string>(json);
            return basket;
        }

        public string GetProductBasket(string UserId)
        {
            var response = client.GetAsync($"/productAndBasket?UserId={UserId}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var basket = JsonConvert.DeserializeObject<string>(json);
            return basket;
        }

        public ResultDto RemoveItemFromBasket(Guid ItemId)
        {
            var response = client.DeleteAsync($"/api/Basket?itemId={ItemId}").Result;
            return ReturnResult(response);
        }

        public ResultDto SetQuantity(int quantity, Guid ItemId)
        {
            var response = client.PutAsync($"/api/Basket?quantity={ItemId}&ItemId={ItemId}", null).Result;
            return ReturnResult(response);
        }

        private ResultDto ReturnResult(HttpResponseMessage response)
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
