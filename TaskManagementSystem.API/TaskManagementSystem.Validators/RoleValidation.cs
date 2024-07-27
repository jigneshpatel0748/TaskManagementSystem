using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManagementSystem.Dto.Role;

namespace TaskManagementSystem.Validators
{
    public class RoleRequestValidator : AbstractValidator<RoleRequestDto>
    {
        public RoleRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is reqired");
        }
    }
}
