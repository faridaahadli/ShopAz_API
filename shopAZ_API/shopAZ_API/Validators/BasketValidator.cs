using FluentValidation;
using shopAZ_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Validators
{
    public class BasketValidator:AbstractValidator<BasketViewModel>
    {
        public BasketValidator()
        {
            RuleFor(x => x.ProductCount)
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.ProductId)
                .NotEmpty();
        }
    }
}
