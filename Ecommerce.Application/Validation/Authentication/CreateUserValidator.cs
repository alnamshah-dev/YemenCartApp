using Ecommerce.Application.DTOs.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Validation.Authentication
{
    public class CreateUserValidator:AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(y => y.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email address.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password is required.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number character.")
                .Matches(@"\w").WithMessage("Password must contain at least one special character.");
            RuleFor(m => m.ConfirmPassword).Equal(y => y.Password).WithMessage("Password do not match.");
        }
    }
}
