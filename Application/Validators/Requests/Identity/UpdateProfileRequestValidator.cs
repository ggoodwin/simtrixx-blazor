using Application.Requests.Identity;
using FluentValidation;

namespace Application.Validators.Requests.Identity
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator()
        {
            RuleFor(request => request.FirstName)
                .NotEmpty().WithMessage(x => "First Name is required");
            RuleFor(request => request.LastName)
                .NotEmpty().WithMessage(x => "Last Name is required");
        }
    }
}
