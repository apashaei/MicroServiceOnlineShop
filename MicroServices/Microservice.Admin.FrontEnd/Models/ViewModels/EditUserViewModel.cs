using Microservice.Admin.FrontEnd.Models.ViewServices.User;

namespace Microservice.Admin.FrontEnd.Models.ViewModels
{
    public class EditUserViewModel
    {
        public UserDto User { get; set; }
        public List<RoleDto> Roles { get; set; }
        public string RoleId { get; set; }
    }
}
