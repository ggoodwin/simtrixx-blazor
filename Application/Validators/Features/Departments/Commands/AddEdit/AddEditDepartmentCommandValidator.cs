using Application.Features.Departments.Commands.AddEdit;
using FluentValidation;

namespace Application.Validators.Features.Departments.Commands.AddEdit
{
    public class AddEditDepartmentCommandValidator : AbstractValidator<AddEditDepartmentCommand>
    {
        public AddEditDepartmentCommandValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage(x => "Name is required!");
            RuleFor(request => request.Description)
                .NotEmpty().WithMessage(x => "Description is required!");
        }
    }
}
