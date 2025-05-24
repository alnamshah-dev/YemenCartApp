using Ecommerce.Application.DTOs.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Validation.Authentication
{
    public class LoginUserValidator:AbstractValidator<LoginUser>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").
                EmailAddress().WithMessage("Invalid email address.");
            RuleFor(y => y.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
