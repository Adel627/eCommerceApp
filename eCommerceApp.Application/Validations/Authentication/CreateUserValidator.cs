using eCommerceApp.Application.DTOs.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Validations.Authentication
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName)
                .Length(5 ,30);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .Matches("^[a-zA-Z0-9-._@+]*$").WithMessage("Username can only contain letters or digits.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .Matches("^01[0,1,2,5]{1}[0-9]{8}$").WithMessage("Invalid mobile number.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 character long. ")
                .Matches(@"[A-Z]").WithMessage("Password contain at least one uppercase letter. ")
                .Matches(@"[a-z]").WithMessage("Password contain at least one lowercase letter. ")
                .Matches(@"\d").WithMessage("Password contain at least one number. ")
                .Matches(@"[^\w]").WithMessage("Password contain at least one speacial character. ");

            RuleFor(x => x.ConfirmPassword)
                     .NotEmpty().WithMessage("Password is required")
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
