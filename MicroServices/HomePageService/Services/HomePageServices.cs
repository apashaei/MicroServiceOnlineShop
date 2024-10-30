using HomPageServices.Context;
using HomPageServices.Entities;
using HomPageServices.Models.Dtos;
using HomPageServices.Validation;

namespace HomPageServices.Services
{
    public class HomePageServices : IHomePageServices
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IGetImageSrc getImageSrc;
        public HomePageServices(DataBaseContext dataBaseContext, IGetImageSrc getImageSrc)
        {
            _dataBaseContext = dataBaseContext;
            this.getImageSrc = getImageSrc;
        }

        public Task<bool> AddHomePageParts(HomePagePartDto homePagePartDto)
        {
            var newHomePagePart = new HomePageParts
            {
                ImageSrc = homePagePartDto.ImageSrc,
                Priority = homePagePartDto.Priority,
                Link = homePagePartDto.Link,
                Title = homePagePartDto.Title,
                Part = homePagePartDto.Part,
            };

            _dataBaseContext.Add(newHomePagePart);
            _dataBaseContext.SaveChanges();
          return Task.FromResult(true);
        }

        public  Task<List<PartGroupDto>> GetHomePageParts()
        {
            var result = _dataBaseContext.HomePageParts.GroupBy(x => x.Part)
                 .Select(group => new PartGroupDto
                 {
                     part = group.Key,
                     Items = group.Select(x => new PartItemDto
                     {
                         Id = x.Id,
                         ImageSrc = getImageSrc.Execute(x.ImageSrc),
                         Priority = x.Priority,
                         Link = x.Link,
                         Title = x.Title,

                     }).ToList()
                 }).ToList();
            if(result == null)
            {
                throw new NotFoundException(nameof(HomePageParts), "");
            }

             return Task.FromResult(result);
        }
    }

    public class PartGroupDto
    {
        public PartEnum part {  get; set; }
        public List<PartItemDto> Items { get; set; }

    }

    public class PartItemDto
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
    }

}
