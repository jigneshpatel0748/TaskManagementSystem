using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManagementSystem.Dto.Task;

namespace TaskManagementSystem.Validators
{
    public class TaskRequestValidator : AbstractValidator<TaskRequestDto>
    {
        public TaskRequestValidator()
        {
            RuleFor(x => x.Subject).NotEmpty().NotNull().WithMessage("Name is reqired");
            RuleFor(x => x.ProjectId).NotEmpty().NotNull().WithMessage("Project is reqired");
            RuleFor(x => x.AssignTo).NotEmpty().NotNull().WithMessage("Assign user is reqired");
            RuleFor(x => x.AssignDate).NotEmpty().NotNull().WithMessage("Assign Date is reqired");
            RuleFor(x => x.Priority).NotEmpty().NotNull().WithMessage("Priority Date is reqired");
            RuleFor(x => x.Type).NotEmpty().NotNull().WithMessage("Type Date is reqired");
            RuleFor(x => x.IsActive).NotNull().WithMessage("Active is reqired");
        }
    }

    public class TaskReportRequestValidator : AbstractValidator<TaskReportRequestDto>
    {
        public TaskReportRequestValidator()
        {
            RuleFor(x => x.AssignFromDate).NotEmpty().NotNull().WithMessage("From date is reqired");
            RuleFor(x => x.AssignToDate).NotEmpty().NotNull().WithMessage("To date is reqired");
        }
    }
}
