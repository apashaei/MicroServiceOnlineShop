using BasketServices.Models.Dtos;
using DiscountServices.Protos;
using Grpc.Net.Client;

namespace BasketServices.Services.DiscountServices
{
    public interface IDiscountServices
    {
        ResultDto<discountDto> GetDiscountByCode(string code);
        ResultDto<discountDto> GetDiscountById(string Id);
    }
    public class DiscountServices : IDiscountServices
    {
        private readonly GrpcChannel grpc;
        public DiscountServices(GrpcChannel grp)
        {
            this.grpc = grp;
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
    }
}
