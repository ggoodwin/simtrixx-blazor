using Application.Features.SupportTickets.Commands.AddEdit;
using FluentValidation;

namespace Application.Validators.Features.SupportTickets.Commands.AddEdit
{
    public class AddEditSupportTicketCommandValidator : AbstractValidator<AddEditSupportTicketCommand>
    {
        public AddEditSupportTicketCommandValidator()
        {
            RuleFor(request => request.Subject)
                .NotEmpty().WithMessage(x => "Subject is required!");
            RuleFor(request => request.Description)
                .NotEmpty().WithMessage(x => "Description is required!");
            RuleFor(request => request.Priority)
                .IsInEnum().WithMessage("Must Choose a Priority");
            RuleFor(request => request.SupportDepartmentId)
                .NotEmpty().WithMessage(x => "Department is required!");
        }
    }
}
