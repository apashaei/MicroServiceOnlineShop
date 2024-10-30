using Microsoft.AspNetCore.Identity;

namespace IdentityService.Models.Entities
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
    }
}
