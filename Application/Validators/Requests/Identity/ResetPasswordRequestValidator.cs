using Application.Requests.Identity;
using FluentValidation;

namespace Application.Validators.Requests.Identity
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage(x => "Email is required")
                .EmailAddress().WithMessage(x => "Email is not correct");
            RuleFor(request => request.Password)
                .NotEmpty().WithMessage(x => "Password is required!")
                .MinimumLength(8).WithMessage("Password must be at least of length 8")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one capital letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit");
            RuleFor(request => request.ConfirmPassword)
                .NotEmpty().WithMessage(x => "Password Confirmation is required!")
                .Equal(request => request.Password).WithMessage(x => "Passwords don't match");
            RuleFor(request => request.Token)
                .NotEmpty().WithMessage(x => "Token is required");
        }
    }
}
