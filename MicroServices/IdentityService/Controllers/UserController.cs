using IdentityService.Models;
using IdentityService.Models.Entities;
using IdentityService.Models.Utitlities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Get(int Page = 1, int PageSize = 20)
        {
            var rowcount = 0;
            var users = userManager.Users.Select(p=> new
            {
                p.Id,
                p.FullName,
                p.Email,
                p.PhoneNumber,
                p.UserName,
            }).ToPaged(Page, PageSize, out rowcount).ToList();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDto user)
        {
            var newUser = new User
            {
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.Username,
                PhoneNumber = user.PhoneNumber,


            };
            var createdUser = userManager.CreateAsync(newUser, user.Password).Result;
            var addedRole = userManager.AddToRoleAsync(newUser, user.Role.Name).Result;

            if (createdUser.Succeeded && addedRole.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var user = userManager.FindByIdAsync(id.ToString()).Result;
            var role = userManager.GetRolesAsync(user).Result;
            var userDto = new UserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                Username = user.UserName,
                Role = new RoleDto
                {
                    Name = role.FirstOrDefault()
                },
                PhoneNumber = user.PhoneNumber,
            };
            return Ok(userDto); 
        }

        [HttpPut]
        public IActionResult Edit([FromBody] UserDto userDto)
        {
            var user = userManager.FindByNameAsync(userDto.Username.ToString()).Result;
            var role = roleManager.FindByNameAsync(userDto.Role.Name).Result;

            user.FullName = userDto.FullName;
            user.UserName = userDto.Username;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Email = userDto.Email;
            var result = userManager.UpdateAsync(user).Result;
            if(result.Succeeded)
            {
                var roleResult =  userManager.AddToRoleAsync(user, userDto.Role.Name);
            }

            return Ok();
        }

    }
}
