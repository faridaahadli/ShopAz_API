using Admin.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Validators
{
    public class ProductValidator:AbstractValidator<ProductCreateViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Price)
            .NotEmpty().WithMessage("This field is required");
            RuleFor(x => x.Seller).NotEmpty().WithMessage("This field is required");
            RuleFor(x => x.StockCode).NotEmpty().WithMessage("This field is required");
            RuleFor(x => x.StockCount).NotEmpty().WithMessage("This field is required");
            RuleFor(x => x.Discount).LessThanOrEqualTo(100).WithMessage("Max value for this field is 100");         
        }
    }
}
