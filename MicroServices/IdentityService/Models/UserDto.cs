namespace IdentityService.Models
{
    public class UserDto
    {

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public RoleDto Role { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class RoleDto
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
