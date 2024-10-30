using Microservice.Admin.FrontEnd.Exceptions;
using Microservice.Admin.FrontEnd.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NuGet.Configuration;
using RestSharp;
using System.Drawing.Printing;
using System.Text;

namespace Microservice.Admin.FrontEnd.Models.ViewServices.User
{
    public interface IUserServices
    {
        Pagenated GetUsers(int page = 1, int pageSize = 20);
        bool AddUser(UserDto userDto);
        List<RoleDto> GetRoles();
        UserDto GetUser(string id);
        bool EditUser(UserDto userDto);
        ResultDto DeleteUser(string id);

    }

    public class UserServices : IUserServices
    {
        private readonly RestClientOptions restClientOptions;
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserServices(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool AddUser(UserDto userDto)
        {

            string serializeModel = System.Text.Json.JsonSerializer.Serialize(userDto);
            var content = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/user", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public ResultDto DeleteUser(string id)
        {
            var response = client.DeleteAsync($"api/User?id={id}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ResultDto>(json);
            return result;
        }

        public bool EditUser(UserDto userDto)
        {
            string serializeModel = System.Text.Json.JsonSerializer.Serialize(userDto);
            var content = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/user", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public List<RoleDto> GetRoles()
        {
            var response = client.GetAsync($"api/User/Roles").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var roles = JsonConvert.DeserializeObject<List<RoleDto>>(json);
            return roles;
        }

        public UserDto GetUser(string id)
        {
            var response = client.GetAsync($"api/User/{id}").Result;
            if(response.StatusCode== System.Net.HttpStatusCode.Forbidden)
            {
                throw new ForbiddenExceptions();
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<UserDto>(json);
            return user;
        }

        public Pagenated GetUsers(int page = 1, int pageSize = 20)
        {

            var token = httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;

            var response = client.GetAsync($"api/User?Page={page}&PageSize={pageSize}").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                throw new ForbiddenExceptions();
            }
            var json = response.Content.ReadAsStringAsync().Result;
            var users = JsonConvert.DeserializeObject<Pagenated>(json);
            return users;
        }
    }

    public class Item
    {
        public string Id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public object password { get; set; }
        public string username { get; set; }
        public object role { get; set; }
        public string phoneNumber { get; set; }
        public int rowCount { get; set; }
    }

    public class Pager
    {
        public List<int> pages { get; set; }
        public int totalPages { get; set; }
    }

    public class Pagenated
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int count { get; set; }
        public List<Item> items { get; set; }
        public Pager pager { get; set; }
        public bool hasPrevPage { get; set; }
        public bool hasNextPage { get; set; }
    }

}
