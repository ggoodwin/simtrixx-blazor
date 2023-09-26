using Application.Requests.Identity;
using FluentValidation;

namespace Application.Validators.Requests.Identity
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage(x => "Email is required")
                .EmailAddress().WithMessage(x => "Email is not correct");
            RuleFor(request => request.Password)
                .NotEmpty().WithMessage(x => "Password is required!");
        }
    }
}
