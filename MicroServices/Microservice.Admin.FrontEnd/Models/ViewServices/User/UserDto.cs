namespace Microservice.Admin.FrontEnd.Models.ViewServices.User
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }

        public string? RoleName { get; set; }
        public RoleDto Role { get; set; }
    }

}
