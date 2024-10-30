using AutoMapper;
using BasketServices.Models.Entities;
using BasketServices.Services;
using static BasketServices.Services.BasketServices;

namespace BasketServices.Infrastructure.MappinProfiles
{
    public class BasketitemMapper:Profile
    {
        public BasketitemMapper()
        {
            CreateMap<AddItemToBaketDto, BasketItems>();
            CreateMap<Productdto, Product>().ReverseMap();
        }
    }
}
