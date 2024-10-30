using HomPageServices.Models.Dtos;

namespace HomPageServices.Services
{
    public interface IHomePageServices
    {
        public Task<bool> AddHomePageParts(HomePagePartDto homePagePartDto);
        public Task<List<PartGroupDto>> GetHomePageParts();
    }

}
