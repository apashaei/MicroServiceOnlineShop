using IdentityModel.Client;
using Microservice.Web.FronEnd.Models.NewFolder;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using NuGet.Configuration;
using RestSharp;

namespace Microservice.Web.FronEnd.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<List<OrderDto>> GetOrders();
        Task<OrderDetailDto> GetOrderById(Guid id);
        Task<ResultDto> SendOrderToPay(Guid orderId);
    }

    public class OrderServices : IOrderServices
    {

        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;
        private string _accessToken = null;

        public OrderServices(HttpClient client,IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }
        private async Task<string> GetaccessToken()
        {
            if(_accessToken != null)
            {
                return _accessToken;
            }
            HttpClient httpClient = new HttpClient();
            var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("https://localhost:7036/");
            var token = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "webfrontend",
                ClientSecret="123456",
                Scope = "orderservice.fullAccess",
            });
            if (token.IsError)
            {
                throw new Exception();
            }
            _accessToken = token.AccessToken;
            return _accessToken;
        }

        public async Task<OrderDetailDto> GetOrderById(Guid id)
        {

            var response = await client.GetAsync($"api/Order/OrderId?OrderId={id}");
            var json = response.Content.ReadAsStringAsync().Result;
            var userOrder = JsonConvert.DeserializeObject<OrderDetailDto>(json);
            return userOrder;

        }

        public async Task<List<OrderDto>> GetOrders()
        {
       
            var accesToken = httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var refreshToken = httpContextAccessor.HttpContext.GetTokenAsync("refresh_token");

            var response = await client.GetAsync(string.Format("/api/Order"));
            var json = response.Content?.ReadAsStringAsync().Result;
            var orders = JsonConvert.DeserializeObject<List<OrderDto>>(json);
            return orders;
            
        }

        public async Task<ResultDto> SendOrderToPay(Guid orderId)
        {
      
            var response = await client.PostAsync($"api/OrderPay?orderId={orderId}",null);
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

    public class OrderLineDto
    {
        public Guid Id { get; set; }
        public Guid ProducId { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDto
    {
        public Guid OrderId { get; set; }

        public DateTime Orderplaced { get; set; }

        public bool Orderpaid { get; set; }
        public int ItemCount { get; set; }
        public int TotalPrice { get; set; }
        public Paymetnstatus paymetnstatus { get; set; }


    }

    public enum Paymetnstatus
    {
        NotPaid = 0,
        RequestPay = 1,
        Ispaied = 2
    }



    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime Orderplaced { get; set; }

        public bool Orderpaid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public Paymetnstatus Paymetnstatus { get; set; }

       public int Totalprice { get; set; }
        public List<OrderLineDto> orderlines { get; set; }


    }


}
