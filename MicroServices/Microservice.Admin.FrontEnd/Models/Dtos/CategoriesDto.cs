namespace Microservice.Admin.FrontEnd.Models.Dtos
{
    public class CategoriesDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public string? Link { get; set; }
        public int? ChildCount { get; set; }
    }
}
