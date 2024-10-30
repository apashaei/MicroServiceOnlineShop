using Microsoft.AspNetCore.Identity;

namespace UserManagerService.Models.Entities
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
    }
}
