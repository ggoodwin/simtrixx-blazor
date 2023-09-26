using System.Security.Claims;
using Application.Features.DemoRequests.Commands.AddEdit;
using Application.Features.DemoRequests.Queries.GetAll;
using Client.Infrastructure.Managers.DemoRequest;
using Common.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Admin.DemoRequests
{
    public partial class ShowDemos
    {
        private bool _loaded;
        [Inject] private IDemoRequestManager? DemoRequestManager { get; set; }

        public List<GetAllDemoRequestsResponse> _demoRequests = new();

        private GetAllDemoRequestsResponse _demoRequest = new();
        private string _searchString = "";
        private ClaimsPrincipal? _currentUser;
        
        private bool _canSearchDemoRequests;
        private bool _canEditDemoRequests;
        private bool _canDeleteDemoRequests;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canSearchDemoRequests = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DemoRequests.Search)).Succeeded;
            _canEditDemoRequests = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DemoRequests.Edit)).Succeeded;
            _canDeleteDemoRequests = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DemoRequests.Delete)).Succeeded;

            await GetDataAsync();
            _loaded = true;
        }

        private async Task Reset()
        {
            await GetDataAsync();
        }

        private async Task GetDataAsync()
        {
            var response = await DemoRequestManager?.GetAllDemoRequestsAsync()!;
            if (response.Succeeded)
            {
                _demoRequests = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool Search(GetAllDemoRequestsResponse response)
        {
            var result = string.IsNullOrWhiteSpace(_searchString);
            
            if (result) return result;
            if (response.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            if (response.Email.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            return result;
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _demoRequest = _demoRequests.FirstOrDefault(c => c.Id == id);
                if (_demoRequest != null)
                {
                    parameters.Add(nameof(AddEditDemoRequestModal.AddEditDemoRequestModel), new AddEditDemoRequestCommand
                    {
                        Id = _demoRequest.Id,
                        Name = _demoRequest.Name,
                        Email = _demoRequest.Email,
                        Contacted = _demoRequest.Contacted
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditDemoRequestModal>(id == 0 ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Delete(int id)
        {
            var deleteContent = "Delete Demo Request";
            var parameters = new DialogParameters
            {
                { nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id) }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await DemoRequestManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await Reset();
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }
    }
}
