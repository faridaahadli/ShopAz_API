using FluentValidation;
using shopAZ_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Validators
{
    public class RegisterValidator:AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("This field is required")
            .EmailAddress().WithMessage("Email Address is not valid")
            .MaximumLength(50);
            RuleFor(x => x.FirstName).NotEmpty()
            .WithMessage("This field is required").MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty()
            .WithMessage("This field is required").MaximumLength(50);
            //Rules for password
            RuleFor(x => x.Password).PasswordChecker();
        }
    }
}
