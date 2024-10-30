using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data
{
    public static class DataBaseContextSeed
    {
        public static void RoleSeed(ModelBuilder builder)
        {
            foreach (var item in RoleSeed())
            {
                builder.Entity<IdentityRole>().HasData(item);
            }
        }
        private static IEnumerable<IdentityRole> RoleSeed()
        {
            return new List<IdentityRole>
            {

                new IdentityRole{ Id="1", Name="Customer"},
                new IdentityRole{ Id="2", Name="Admin"},
            };
        }
    }

   
}
