using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Context;

namespace TaskManagementSystem.Data.Seeds
{
    public static class UserSeeds
    {
        public static void AddDefaultUsers(DataContext context)
        {
            string[] adminUsers = new[] { "admin@gmail.com" };
            foreach (var item in adminUsers)
            {
                if (!context.Users.Any(x => x.Email == item))
                {
                    var adminId = context.Roles.FirstOrDefault(x => x.Name == "Admin")?.Id;
                    context.Users.Add(new Entity.Users
                    {
                        Email = item,
                        Password = BCrypt.Net.BCrypt.HashPassword("Admin@123", 10),
                        IsActive = true,
                        RoleId = Convert.ToInt32(adminId),
                        FullName = "Admin"
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
