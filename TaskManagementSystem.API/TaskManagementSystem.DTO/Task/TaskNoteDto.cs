using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Dto.Task
{
    public class TaskNoteDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Note { get; set; }
        public string? Attachment { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
