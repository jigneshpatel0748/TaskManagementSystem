using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Dto.Task
{
    public class TaskTrackerDto
    {
        public int TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EmdTime { get; set; }
    }
}
