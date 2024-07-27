using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManagementSystem.Dto.Project;

namespace TaskManagementSystem.Validators
{
    public class ProjectRequestValidator : AbstractValidator<ProjectRequestDto>
    {
        public ProjectRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is reqired");
        }
    }
}
