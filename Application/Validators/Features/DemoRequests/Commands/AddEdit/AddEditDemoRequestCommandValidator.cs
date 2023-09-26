using Application.Features.DemoRequests.Commands.AddEdit;
using FluentValidation;

namespace Application.Validators.Features.DemoRequests.Commands.AddEdit
{
    public class AddEditDemoRequestCommandValidator : AbstractValidator<AddEditDemoRequestCommand>
    {
        public AddEditDemoRequestCommandValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage(x => "Name is required!");
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage(x => "Email is required!")
                .EmailAddress().WithMessage("A Valid Email Address is required!");
        }
    }
}
