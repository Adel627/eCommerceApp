using eCommerceApp.Application.DTOs.Cart;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Validations.Cart
{
    public class ProcessCartValidator : AbstractValidator<ProcessCart>
    {
        public ProcessCartValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty()
                .GreaterThan(0).WithMessage("Quantity must be grather than 0");
        }
    }
}
