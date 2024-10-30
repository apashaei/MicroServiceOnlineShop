using AutoMapper;
using DiscountServices.Models.Entities;
using DiscountServices.Services;

namespace DiscountServices.Infrastructure.MappingProfiles
{
    public class DicountMappingProfile:Profile
    {
        public DicountMappingProfile()
        {
            CreateMap<Discount,DiscountDto>().ReverseMap();
        }
    }
}
