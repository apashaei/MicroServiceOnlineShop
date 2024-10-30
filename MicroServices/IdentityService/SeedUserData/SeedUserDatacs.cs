using IdentityService.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace IdentityService.SeedUserData
{
    public static class SeedUserDatacs
    {
        public static void Seed(IApplicationBuilder application)
        {
            using (var servicescope = application.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = servicescope.ServiceProvider.GetRequiredService<UserManager<User>>(); 
                if(context.Users.Count() ==0)
                {
                    IdentityRole identityRoleAdmin = new IdentityRole
                    {
                        Name = "Admin",
                    };

                    IdentityRole identityRoleCustomer = new IdentityRole
                    {
                        Name = "Customer",
                    };
                    var rolecontext = servicescope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    rolecontext.CreateAsync(identityRoleAdmin).Wait();
                    rolecontext.CreateAsync(identityRoleCustomer).Wait();
                    foreach (var user in UserList())
                    {
                        var result = context.CreateAsync(user, "123456A@aa").Result;
                        if (user.UserName == "Ali")
                        {
                            var addToAdminRole = context.AddToRoleAsync(user, "Admin").Result;
                        }
                        else
                        {
                            var addToCustomerRole = context.AddToRoleAsync(user, "Customer").Result;

                        }
                    }
                }
            }
        }

        private static List<User> UserList()
        {
            return new List<User>()
            {
                new User
                    {
                    UserName = "Ali",
                    Email = "alipashaei88@gmail.com",
                    EmailConfirmed = true,
                },
                new User
                {
                    UserName = "Test",
                    Email = "Test88@gmail.com",
                    EmailConfirmed = true,
                },
            };
        }
    }
}
