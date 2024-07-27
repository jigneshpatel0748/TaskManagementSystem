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
    public class TaskNotes : TimeStamp
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Note { get; set; }
        public string? Attachment { get; set; }
        [ForeignKey("TaskId")]
        public virtual Tasks? Task { get; set; }
    }
}
