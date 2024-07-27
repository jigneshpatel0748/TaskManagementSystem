using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManagementSystem.Dto.Task;

namespace TaskManagementSystem.Validators
{
    public class TaskNoteRequestValidation : AbstractValidator<TaskNoteRequestDto>
    {
        public TaskNoteRequestValidation()
        {
            RuleFor(x => x.Note).NotEmpty().NotNull().WithMessage("Note is reqired");
        }
    }
}
