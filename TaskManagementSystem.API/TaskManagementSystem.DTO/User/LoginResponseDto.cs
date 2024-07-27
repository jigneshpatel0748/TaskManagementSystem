﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Role;

namespace TaskManagementSystem.Dto.User
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public int ManagerId { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }
        public RoleDto Role { get; set; }
    }
}
