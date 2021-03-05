using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Validators
{
    public static class ValidatorExtension
    {
        public static IRuleBuilder<T,string> PasswordChecker<T>(this IRuleBuilder<T,string> ruleBuilder)
        {
            var options=ruleBuilder.NotEmpty()
            .WithMessage("This field is required").MinimumLength(8)
            .WithMessage("Password must contain at least 8 characaters")
            .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain 1 lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain 1 alphanumeric character");
            return options;
        }
    }
}
