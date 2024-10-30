﻿namespace UserManagerService.Models.Dtos
{
    public class UserDto
    {

        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string Username { get; set; }
        public RoleDto Role { get; set; }
        public string PhoneNumber { get; set; }
        public int RowCount { get; set; }
    }
}
