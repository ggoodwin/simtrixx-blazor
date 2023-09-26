using Application.Features.ContactRequests.Commands.AddEdit;
using Blazored.FluentValidation;
using Client.Infrastructure.Managers.ContactRequest;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Admin.ContactRequests
{
    public partial class AddEditContactRequestModal
    {
        [Inject] private IContactRequestManager? ContactRequestManager { get; set; }

        [Parameter] public AddEditContactRequestCommand AddEditContactRequestModel { get; set; } = new();
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
            var response = await ContactRequestManager.SaveAsync(AddEditContactRequestModel);
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
