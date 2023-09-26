using Application.Features.DemoRequests.Commands.AddEdit;
using Blazored.FluentValidation;
using Client.Extensions;
using Client.Infrastructure.Managers.DemoRequest;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Communication
{
    public partial class Demo
    {
        private FluentValidationValidator? _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator != null && _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private AddEditDemoRequestCommand _demoRequestModel = new();
        [Inject] IDemoRequestManager DemoRequestManager { get; set; }
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
                _demoRequestModel.Email = user.GetEmail();
                _demoRequestModel.Name = $"{user.GetFirstName()} {user.GetLastName()}";
                _fieldDisabled = true;
            }
        }

        private async Task SubmitAsync()
        {
            _disabled = true;
            _demoRequestModel.Contacted = false;
            var response = await DemoRequestManager.SaveAsync(_demoRequestModel);
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
