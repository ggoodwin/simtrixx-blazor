using Application.Requests.Identity;
using FluentValidation;

namespace Application.Validators.Requests.Identity
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage(x => "Email is required")
                .EmailAddress().WithMessage(x => "Email is not correct");
        }
    }
}
