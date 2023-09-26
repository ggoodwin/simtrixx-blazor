using Application.Features.ContactRequests.Commands.AddEdit;
using Blazored.FluentValidation;
using Client.Extensions;
using Client.Infrastructure.Managers.ContactRequest;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Communication
{
    public partial class ContactUs
    {
        private FluentValidationValidator? _fluentValidationValidator;
        private readonly AddEditContactRequestCommand _contactRequestModel = new();
        [Inject] private IContactRequestManager? ContactRequestManager { get; set; }
        private bool _disabled;
        private bool _loaded;
        private bool _fieldDisabled;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            _loaded = true;
        }

        private async Task LoadData()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if (user is { Identity: { IsAuthenticated: true } })
            {
                _contactRequestModel.Email = user.GetEmail();
                _contactRequestModel.Name = $"{user.GetFirstName()} {user.GetLastName()}";
                _fieldDisabled = true;
            }
        }

        private async Task SubmitAsync()
        {
            _disabled = true;
            _contactRequestModel.Contacted = false;
            var response = await ContactRequestManager?.SaveAsync(_contactRequestModel)!;
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
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
