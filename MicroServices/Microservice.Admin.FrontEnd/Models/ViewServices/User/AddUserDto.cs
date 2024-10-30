namespace Microservice.Admin.FrontEnd.Models.ViewServices.User
{
    public class EditUserDto
    {

        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
     
        public string Username { get; set; }
        public int roleId { get; set; }
        public string PhoneNumber { get; set; }
    }

}
