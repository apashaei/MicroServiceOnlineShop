using Microservice.Admin.FrontEnd.Models.Dtos;
using Microservice.Admin.FrontEnd.Models.ViewModels;
using Microservice.Admin.FrontEnd.Models.ViewServices.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;

namespace Microservice.Admin.FrontEnd.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserServices userServices;

        public UserController(IUserServices userServices)
        {
            this.userServices = userServices;
        }
        public IActionResult Index()
        
        
        {
            var data = userServices.GetUsers();

            return View(data);
        }
        
        public IActionResult Create()
        {

            
            var roles = userServices.GetRoles();

            if (roles == null || roles.Count == 0)
            {
                roles = new List<RoleDto>
                {
                    new RoleDto
                    {
                        Id = "1",
                        Name = "Admin",
                    },
                    new RoleDto
                    {
                        Id = "2",
                        Name = "Customer",
                    }
                };
            }

            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddUserViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

                var newUserDto = new UserDto
                {
                    Email = viewModel.Email,
                    FullName = viewModel.FullName,
                    Id = viewModel.Id,
                    Password = viewModel.Password,
                    PhoneNumber = viewModel.PhoneNumber,
                    RoleName = viewModel.RoleName,
                    UserName = viewModel.UserName,
                };
                var result = userServices.AddUser(newUserDto);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "خطایی رخ داده است";
                    return View();
                }
            
        }

        public IActionResult Edit(string id)
        {
            var user = userServices.GetUser(id);
            user.Id = id;
            var roles = userServices.GetRoles();

            var EditView = new EditUserViewModel
            {
                Roles = roles,
                User = user,
            };
           
            return View(EditView);
        }

        public IActionResult Remove(string id)
        {
            var result = userServices.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel editUser)
        {
            var data = userServices.EditUser(new UserDto
            {
                Email = editUser.User.Email,
                FullName = editUser.User.FullName,
                PhoneNumber = editUser.User.PhoneNumber,
                UserName = editUser.User.UserName,
                Role = new RoleDto
                {
                    Id = editUser.RoleId,
                    Name = ""
                },
                Id = editUser.User.Id,
                Password = "",


            });
            if (data)
            {
                return RedirectToAction("Index");
            }
            return View(editUser);
        }
    }
}
