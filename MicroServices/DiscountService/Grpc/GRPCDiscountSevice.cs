using DiscountServices.Protos;
using DiscountServices.Services;
using Grpc.Core;

namespace DiscountServices.Grpc
{
    public class GRPCDiscountSevice:DiscountServicesProto.DiscountServicesProtoBase
    {
        private readonly IDiscountServices discountServices;

        public GRPCDiscountSevice(IDiscountServices discountServices)
        {
            this.discountServices = discountServices;
        }
        public override Task<ResponseAddNewDiscount> AddNewDiscount(RequestAddNewDiscount request, ServerCallContext context)
        {
            discountServices.AddNewDiscount(new DiscountDto
            {
                Amount = request.Amount,
                Code = request.Code,
                Used = request.Used,
            });
            return Task.FromResult(new ResponseAddNewDiscount
            {
                IsSuccess = true,
            });
        }


        public override Task<ResponseGetDiscountBycode> GetDiscountByCode(RequestGetDiscountBycode request, ServerCallContext context)
        {
            var data = discountServices.GetDiscountByCode(request.Code);
            if (data == null)
            {
                return Task.FromResult(new ResponseGetDiscountBycode
                {
                    IsSuccess = false,
                    Message = "تخفیف یافت نشد.",
                    GetDiscount = null

                });
            }
            return Task.FromResult(new ResponseGetDiscountBycode
            {
                IsSuccess = true,
                Message = "",
                GetDiscount = new GetDiscountBycode
                {
                    Code = data.Code,
                    Amount = data.Amount,
                    Used = data.Used,
                    Id = data.Id.ToString(),
                }

            });
        }

        public override Task<ResponseGetDiscountBycode> GetDiscountById(RequestGetDiscountById request, ServerCallContext context)
        {
            var data = discountServices.GetDiscountById(Guid.Parse(request.Id));
            if (data == null)
            {
                return Task.FromResult(new ResponseGetDiscountBycode
                {
                    IsSuccess = false,
                    Message = "تخفیف یافت نشد.",
                    GetDiscount = null

                });
            }
            return Task.FromResult(new ResponseGetDiscountBycode
            {
                IsSuccess = true,
                Message = "",
                GetDiscount = new GetDiscountBycode
                {
                    Code = data.Code,
                    Amount = data.Amount,
                    Used = data.Used,
                    Id = data.Id.ToString(),
                }

            });
        }

        public override Task<ResponseUseDiscount> UseDiscount(RequestUseDiscount request, ServerCallContext context)
        {
            var result = discountServices.UseDiscount(request.Id);
            return Task.FromResult(new ResponseUseDiscount
            {
                Issuccess = true,
            });
        }
    }
}
