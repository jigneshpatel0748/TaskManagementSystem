using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Base;

namespace TaskManagementSystem.Data.Entity
{
    public class Users : TimeStamp
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public int? ManagerId { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("RoleId")]
        public virtual Roles Role { get; set; }
        [ForeignKey("ManagerId")]
        public virtual Users? Manager { get; set; }
    }
}
