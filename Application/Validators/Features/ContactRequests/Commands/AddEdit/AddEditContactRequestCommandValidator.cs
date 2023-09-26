using Application.Features.ContactRequests.Commands.AddEdit;
using FluentValidation;

namespace Application.Validators.Features.ContactRequests.Commands.AddEdit
{
    public class AddEditContactRequestCommandValidator : AbstractValidator<AddEditContactRequestCommand>
    {
        public AddEditContactRequestCommandValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage(x => "Name is required!");
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage(x => "Email is required!")
                .EmailAddress().WithMessage("A Valid Email Address is required!");
            RuleFor(request => request.Message)
                .NotEmpty().WithMessage(x => "Message is required!");
        }
    }
}
