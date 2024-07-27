using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Enums;

namespace TaskManagementSystem.Dto.Task
{
    public class TaskRequestDto
    {
        public string Subject { get; set; }
        public int ProjectId { get; set; }
        public int? AssignTo { get; set; }
        public Priority Priority { get; set; }
        public TaskType Type { get; set; }
        public DateTime? AssignDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Description { get; set; }
        public string? Attachment { get; set; }
        public Status? Status { get; set; }
        public bool IsActive { get; set; }
    }
}
