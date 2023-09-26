using Application.Features.DemoRequests.Commands.AddEdit;
using Blazored.FluentValidation;
using Client.Infrastructure.Managers.DemoRequest;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Admin.DemoRequests
{
    public partial class AddEditDemoRequestModal
    {
        [Inject] private IDemoRequestManager? DemoRequestManager { get; set; }

        [Parameter] public AddEditDemoRequestCommand AddEditDemoRequestModel { get; set; } = new();
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
            var response = await DemoRequestManager.SaveAsync(AddEditDemoRequestModel);
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
