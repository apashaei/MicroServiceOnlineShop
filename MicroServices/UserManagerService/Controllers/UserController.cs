using App.Metrics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserManagerService.MessagingBus.SendMessage;
using UserManagerService.Models.Dtos;
using UserManagerService.Models.Entities;


namespace UserManagerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMessageBus messageBus;
        private string  queueName_EditUser;
        private string  queueName_AddUser;
        private string  queueName_DeleteUser;
        private ILogger<UserController> logger;
        private readonly IMetrics metrics;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager,IMessageBus messageBus,
            IOptions<RabbitMqConfiguration> rabbitMqConfig, ILogger<UserController> logger,IMetrics metrics)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.messageBus = messageBus;
            this.queueName_EditUser = rabbitMqConfig.Value.QueueName_EditUser;
            this.queueName_AddUser = rabbitMqConfig.Value.QueueName_AddUser;
            this.queueName_DeleteUser = rabbitMqConfig.Value.QueueName_DeleteUser;
            this.logger = logger;
            this.metrics = metrics;
        }

        [HttpGet]
        public IActionResult Get(int Page = 1, int PageSize = 20)
        {
            logger.LogInformation("This is from User controller");
            logger.LogError("This is from User controller");


            //metrics.Measure.Counter.Increment(new App.Metrics.Counter.CounterOptions
            //{
            //    Name = "Get_List_Users"
            //});

            var rowcount = 0;
            var data = userManager.Users.ToList();
            var users = userManager.Users.Select(p => new UserDto
            {
                Id = p.Id,
                Email = p.Email,
                FullName = p.FullName,
                PhoneNumber = p.PhoneNumber,
                Username = p.UserName,
                
            }).ToList();
           return Ok(new PagenatedItemDto<UserDto>(Page,PageSize,rowcount,users));
        }

        [HttpGet("Roles")]

        public IActionResult Get()
        {
            var roles = roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpDelete]
        public IActionResult Delete(string Id)
        {
            var user = userManager.FindByIdAsync(Id).Result;
            if (user != null)
            {
                var result = userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    var removeUser = new RemoveUserMessageDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                    };
                    messageBus.SendMessage(removeUser, queueName_DeleteUser);
                    return Ok(new ResultDto
                    {
                        IsSuccess = true,
                        Message="کاربر با موفقیت حذف شد."
                    });
                }
                return BadRequest(new ResultDto
                {
                    IsSuccess = false,
                    Message = "در حذف کاربر مشکلی پیش امده است."
                });
            }
            return BadRequest(new ResultDto
            {
                IsSuccess= false,
                Message="کاربر یافت نشد."
            });
            
        }

        [HttpPost]
        public IActionResult Post( AddUserDto user)
        {
            var newUser = new User
            {
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,

            };

           
            var createdUser = userManager.CreateAsync(newUser, user.Password).Result;
            if (roleManager.RoleExistsAsync(user.RoleName).Result)
            {

                var addedRole = userManager.AddToRoleAsync(newUser, user.RoleName).Result;
                if (createdUser.Succeeded && addedRole.Succeeded)
                {
                    AddUserMessage addUserMessage = new AddUserMessage
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        FullName = user.FullName,
                        Password = user.Password,
                        Id = newUser.Id,
                        Role = new UserRoleMessage
                        {
                            Name = user.RoleName,

                        }
                    };
                    messageBus.SendMessage(addUserMessage,queueName_AddUser);

                    return Ok();
                }
            }

            else
            {
                var role = roleManager.CreateAsync(new IdentityRole
                {
                    Name = user.RoleName,
                }).Result;
                var addedRole = userManager.AddToRoleAsync(newUser, user.RoleName).Result;
                if (createdUser.Succeeded && addedRole.Succeeded)
                {
                    AddUserMessage addUserMessage = new AddUserMessage
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        FullName = user.FullName,
                        Password = user.Password,
                        Id = newUser.Id,
                        Role = new UserRoleMessage
                        {
                            Name = user.RoleName

                        }
                    };
                    messageBus.SendMessage(addUserMessage, queueName_AddUser);
                    return Ok();
                }
            }
            return BadRequest();
        }


        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {



            metrics.Measure.Counter.Increment(new App.Metrics.Counter.CounterOptions
            {
                Name = "Get_Details"
            });

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

            var user = userManager.FindByIdAsync(userDto.Id.ToString()).Result;
            var role = roleManager.FindByIdAsync(userDto.Role.Id).Result;

            
            var originalFullName = user.FullName;
            var originalUsername = user.UserName;
            var originalPhoneNumber = user.PhoneNumber;
            var originalEmail = user.Email;

            user.FullName = userDto.FullName;
            user.UserName = userDto.Username;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Email = userDto.Email;

            var result = userManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            {
                // Try to assign the new role
                var roleResult = userManager.AddToRoleAsync(user, role.Name).Result;
                if (roleResult.Succeeded || roleResult.Errors.Any(p=>p.Code== "UserAlreadyInRole"))
                {
                    // Role assignment succeeded, send the message to RabbitMQ
                    var editUserMessage = new EditUserMessage
                    {
                        Email = userDto.Email,
                        FullName = userDto.FullName,
                        phoneNumber = userDto.PhoneNumber,
                        RoleName = role.Name,
                        Username = userDto.Username
                    };
                    messageBus.SendMessage(editUserMessage, queueName_EditUser);

                    return Ok(); // Everything succeeded
                }
                else
                {
                    // Role assignment failed, revert user changes
                    user.FullName = originalFullName;
                    user.UserName = originalUsername;
                    user.PhoneNumber = originalPhoneNumber;
                    user.Email = originalEmail;

                    var revertResult = userManager.UpdateAsync(user).Result;
                    if (!revertResult.Succeeded)
                    {
                        // Handle the failure of reverting the user data (log it or return error)
                        return StatusCode(500, "Error while reverting user changes.");
                    }

                    return StatusCode(500, "Role assignment failed, user update was reverted.");
                }
            }

            return StatusCode(500, "User update failed.");
        }

        public class EditUserMessage : BaseMessage
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string phoneNumber { get; set; }
            public string RoleName { get; set; }
        }

        public class AddUserMessage : BaseMessage
        {
            public string? Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string UserName { get; set; }
            public UserRoleMessage Role { get; set; }
            public string Password { get; set; }
        }
        public class UserRoleMessage : BaseMessage
        {
            public string Name { get; set; }
            public string? Id { get; set; }
        }
        public class RemoveUserMessageDto : BaseMessage
        {
            public string? Id { get; set; }
            public string? Email { get; set; }
        }
    }
}
