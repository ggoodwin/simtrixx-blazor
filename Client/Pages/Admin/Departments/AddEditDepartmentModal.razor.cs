using Application.Features.Departments.Commands.AddEdit;
using Blazored.FluentValidation;
using Client.Infrastructure.Managers.Department;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Admin.Departments
{
    public partial class AddEditDepartmentModal
    {
        [Inject] private IDepartmentManager? DepartmentManager { get; set; }

        [Parameter] public AddEditDepartmentCommand AddEditDepartmentModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance? MudDialog { get; set; }

        private FluentValidationValidator? _fluentValidationValidator;
        private bool _disabled;

        public void Cancel()
        {
            MudDialog?.Cancel();
        }

        private async Task SaveAsync()
        {
            _disabled = true;
            var response = await DepartmentManager.SaveAsync(AddEditDepartmentModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog?.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}
