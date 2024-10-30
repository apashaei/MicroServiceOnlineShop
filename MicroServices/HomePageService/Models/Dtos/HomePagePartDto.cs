using HomPageServices.Entities;

namespace HomPageServices.Models.Dtos
{
    public class HomePagePartDto
    {
        public int Id { get; set; }

        public PartEnum Part { get; set; }
        public string ImageSrc { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
    }
}
