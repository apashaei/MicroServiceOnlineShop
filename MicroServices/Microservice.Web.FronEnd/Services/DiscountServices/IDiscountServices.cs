using DiscountServices.Protos;
using Grpc.Net.Client;
using Microservice.Web.FronEnd.Models.NewFolder;


namespace Microservice.Web.FronEnd.Services.DiscountServices
{
    public interface IDiscountServices
    {
        ResultDto<discountDto> GetDiscountByCode(string code);  
        ResultDto<discountDto> GetDiscountById(string Id);
        ResultDto UsedDiscount(string code);

    }

    public class DiscountService : IDiscountServices
    {
        private readonly GrpcChannel grpc;
        private readonly IConfiguration configuration;

        public DiscountService(GrpcChannel grpc,IConfiguration configuration)
        {
            


            this.grpc = grpc;
            this.configuration = configuration;
        }
        public ResultDto<discountDto> GetDiscountByCode(string code)
        {
            var grpc_discountService = new DiscountServicesProto.DiscountServicesProtoClient(grpc);
            var data = grpc_discountService.GetDiscountByCode(new RequestGetDiscountBycode
            {
                Code = code
            });
            if (data.IsSuccess)
            {
                return new ResultDto<discountDto>
                {
                    IsSuccess = true,
                    Message = "",
                    Data = new discountDto
                    {
                        Amount = data.GetDiscount.Amount,
                        Code = data.GetDiscount.Code,
                        Id = Guid.Parse(data.GetDiscount.Id),
                        Used = data.GetDiscount.Used,
                    }
                };
            }
            else
            {
                return new ResultDto<discountDto>
                {
                    IsSuccess = false,
                    Message = data.Message,
                    
                };
            }
        }

        public ResultDto<discountDto> GetDiscountById(string Id)
        {
            var grpc_discountService = new DiscountServicesProto.DiscountServicesProtoClient(grpc);
            var data = grpc_discountService.GetDiscountById(new RequestGetDiscountById
            {
                Id = Id
            });
            if (data.IsSuccess)
            {
                return new ResultDto<discountDto>
                {
                    IsSuccess = true,
                    Message = "",
                    Data = new discountDto
                    {
                        Amount = data.GetDiscount.Amount,
                        Code = data.GetDiscount.Code,
                        Id = Guid.Parse(data.GetDiscount.Id),
                        Used = data.GetDiscount.Used,
                    }
                };
            }
            else
            {
                return new ResultDto<discountDto>
                {
                    IsSuccess = false,
                    Message = data.Message,

                };
            }
        }

        public ResultDto UsedDiscount(string Id)
        {
            var grpc_discountService = new DiscountServicesProto.DiscountServicesProtoClient(grpc);
            var data = grpc_discountService.UseDiscount(new RequestUseDiscount
            {
                Id = Id
            });

            return new ResultDto
            {
                IsSuccess = data.Issuccess,
            };
        }
    }

    public class discountDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }

        public bool Used { get; set; }
    }
}
