﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Enums;

namespace TaskManagementSystem.Dto.Task
{
    public class TaskReportRequestDto
    {
        public int? ProjectId { get; set; }
        public int? AssignTo { get; set; }
        public TaskType? Type { get; set; }
        public Priority? Priority { get; set; }
        public DateTime AssignFromDate { get; set; }
        public DateTime AssignToDate { get; set; }
        public bool DueTask { get; set; }
        public Status? Status { get; set; }
    }
}
