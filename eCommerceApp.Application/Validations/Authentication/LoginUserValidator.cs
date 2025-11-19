using eCommerceApp.Application.DTOs.Authentication;
using FluentValidation;

namespace eCommerceApp.Application.Validations.Authentication
{
    public class LoginUserValidator:AbstractValidator<LoginUser> 
    {
        public LoginUserValidator()
        {

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage("Password is required");

        }
    }
}
