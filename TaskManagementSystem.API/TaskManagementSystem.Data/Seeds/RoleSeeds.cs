using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Context;

namespace TaskManagementSystem.Data.Seeds
{
    public static class RoleSeeds
    {
        public static void AddDefaultRoles(DataContext context)
        {
            string[] adminUsers = new[] { "Admin", "Manager", "Team Leader", "Devloper" };
            foreach (var item in adminUsers)
            {
                if (!context.Roles.Any(x => x.Name == item))
                {
                    context.Roles.Add(new Entity.Roles
                    {
                        Name = item,
                        IsActive = true
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
