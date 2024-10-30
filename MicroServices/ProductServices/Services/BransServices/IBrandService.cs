using ProductServices.Models.Context;
using ProductServices.Models.Dtos;

namespace ProductServices.Services.BransServices
{
    public interface IBrandService
    {
        public Task<ResultDto<List<BrandListDto>>> Getbrand();
    }

    public class BrandService : IBrandService
    {
        private readonly DataBaseContext dataBaseContext;
        public BrandService(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }
        public async Task<ResultDto<List<BrandListDto>>> Getbrand()
        {
            var result = dataBaseContext.Brands.Select(p=> new BrandListDto
            {
                Id = p.Id.ToString(),
                Name = p.Name,
            }).ToList();
            return new ResultDto<List<BrandListDto>>
            {
                Data = result,
                Success = true,
                Message = ""
            };
        }
    }
}
