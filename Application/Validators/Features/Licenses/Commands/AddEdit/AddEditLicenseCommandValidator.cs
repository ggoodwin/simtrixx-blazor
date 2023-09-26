using Application.Features.Licenses.Commands.AddEdit;
using FluentValidation;

namespace Application.Validators.Features.Licenses.Commands.AddEdit
{
    public class AddEditLicenseCommandValidator : AbstractValidator<AddEditLicenseCommand>
    {
        public AddEditLicenseCommandValidator()
        {
            RuleFor(request => request.Key)
                .NotEmpty().WithMessage(x => "Key is required!");
            RuleFor(request => request.SimtrixxUserId)
                .NotEmpty().WithMessage(x => "User is required!");
        }
    }
}
