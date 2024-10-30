using HomPageServices.Attributes;

namespace HomPageServices.Entities
{

  

    [Auditable]
    public class HomePageParts
    {
        public int Id { get; set; }

        public PartEnum Part { get; set; }
        public string ImageSrc { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
    }

    public enum PartEnum
    {
       Part0 = 0,
       part1 = 1,
       part2 = 2,
       part3 = 3,
       part4 = 4,
    }


}
