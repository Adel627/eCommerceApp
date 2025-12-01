using eCommerceApp.Application.DTOs.Review;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Validations.Review
{
    public class RateRequestValidator : AbstractValidator<RateRequest>
    {
        public RateRequestValidator()
        {
            RuleFor(r => r.ProductId).NotEmpty();

            RuleFor(x => x.Rate)
                .NotEmpty()
                .GreaterThan(-1).WithMessage("Rate must  be from 0 to 5")
                .LessThan(6).WithMessage("Rate must  be from 0 to 5");
              
        }
    }
}
