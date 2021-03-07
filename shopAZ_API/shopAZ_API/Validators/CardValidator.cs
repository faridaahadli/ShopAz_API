using FluentValidation;
using shopAZ_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Validators
{
    public class CardValidator:AbstractValidator<CardData>
    {
        public CardValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Card number field is required")
                .Length(16).WithMessage("Card number length not accepted");
            RuleFor(x => x.ExpMonth)
                .InclusiveBetween(DateTime.Now.Month, 12).WithMessage("Card was expired");
            RuleFor(x => x.ExpYear)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("Card was expired");
            RuleFor(x => x.Cvc)
                .Length(3).WithMessage("Accepted Cvc length is 3");
        }
    }
}
