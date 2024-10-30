using Microservice.Web.FronEnd.Models.NewFolder;
using Newtonsoft.Json;
using RestSharp;

namespace Microservice.Web.FronEnd.Services.PaymentServices
{
    public interface IPaymentServices
    {
        ResultDto<string> SentToPayment(Guid OrderId, string CallbackUrl);
    }

    public class PaymentServices : IPaymentServices
    {
        private readonly RestClientOptions restClientOptions;

        public PaymentServices(RestClientOptions restClientOptions)
        {
            this.restClientOptions = restClientOptions;
            
            restClientOptions.MaxTimeout = -1;
        }
        public ResultDto<string> SentToPayment(Guid OrderId, string CallbackUrl)
        {
            var client = new RestClient(restClientOptions);

            var request = new RestRequest($"api/Pay?OrderId={OrderId}&CallBackUrl={CallbackUrl}", Method.Get);
            RestResponse response = client.Execute(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<ResultDto<String>>(response.Content);
                return result;
            }
            return new ResultDto<string>
            {
                IsSuccess = false,
                Message = "",
                Data = null
            };
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
