using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Base;
using TaskManagementSystem.Data.Enums;

namespace TaskManagementSystem.Data.Entity
{
    public class Tasks : TimeStamp
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public int ProjectId { get; set; }
        public int AssignTo { get; set; }
        public TaskType Type { get; set; }
        public Priority Priority { get; set; }
        public DateTime AssignDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Description { get; set; }
        public string? Attachment { get; set; }
        public Status? Status { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("AssignTo")]
        public virtual Users AssignUsers { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Projects Projects { get; set; }
    }
}
