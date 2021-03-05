using FluentValidation;
using shopAZ_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Validators
{
    public class LoginValidator:AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("This field is required")
                .EmailAddress().WithMessage("Email Adress is not valid");
            RuleFor(x => x.Password).PasswordChecker();

        }
    }
}
