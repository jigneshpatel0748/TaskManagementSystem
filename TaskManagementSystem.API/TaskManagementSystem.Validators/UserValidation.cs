using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManagementSystem.Dto.Role;
using TaskManagementSystem.Dto.User;

namespace TaskManagementSystem.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().NotNull().WithMessage("Name is reqired");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Name is reqired");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid Email is reqired");
            RuleFor(x => x.RoleId).NotEmpty().NotNull().WithMessage("Role is reqired");
        }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Name is reqired");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid Email is reqired");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password is reqired");
        }
    }
}
