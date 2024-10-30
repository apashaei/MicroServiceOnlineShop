using Microservice.Web.FronEnd.Models.NewFolder;
using Newtonsoft.Json;


namespace Microservice.Web.FronEnd.Services.DiscountServices
{
    public interface IDiscountServicesRestfull
    {
        ResultDto<discountDto> GetDiscountByCode(string code);
        ResultDto<discountDto> GetDiscountById(string Id);
        ResultDto UsedDiscount(string code);
    }
    public class DiscountServicesRestfull : IDiscountServicesRestfull
    {
        private readonly HttpClient httpClient;
        public DiscountServicesRestfull(HttpClient client)
        {
            httpClient = client;
        }
        public ResultDto<discountDto> GetDiscountByCode(string code)
        {
            var response = httpClient.GetAsync($"/api/Discount/code?Code={code}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var discount = JsonConvert.DeserializeObject<ResultDto<discountDto>>(json);
            return discount;

            return new ResultDto<discountDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "خطایی رخ داده است."

            };
        }

        public ResultDto<discountDto> GetDiscountById(string Id)
        {
            var response = httpClient.GetAsync($"api/Discount?Id={Id}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var discount = JsonConvert.DeserializeObject<discountDto>(json);
            if (response.IsSuccessStatusCode)
            {
                return new ResultDto<discountDto>
                {
                    IsSuccess = true,
                    Data = new discountDto
                    {
                        Amount = discount.Amount,
                        Code = discount.Code,
                        Id = discount.Id,
                        Used = discount.Used

                    }
                };
            }

            return new ResultDto<discountDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "خطایی رخ داده است."

            };
        }

        public ResultDto UsedDiscount(string code)
        {
            var response = httpClient.GetAsync($"api/Discount/UsedDiscount?code={code}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var discount = JsonConvert.DeserializeObject<ResultDto>(json);
            return discount;
        }
    }
    }
