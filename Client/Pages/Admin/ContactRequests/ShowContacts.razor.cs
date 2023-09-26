using System.Security.Claims;
using Application.Features.ContactRequests.Commands.AddEdit;
using Application.Features.ContactRequests.Queries.GetAll;
using Client.Infrastructure.Managers.ContactRequest;
using Common.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Admin.ContactRequests
{
    public partial class ShowContacts
    {
        private bool _loaded;
        [Inject] private IContactRequestManager? ContactRequestManager { get; set; }

        public List<GetAllContactRequestsResponse> _contactRequests = new();

        private GetAllContactRequestsResponse _contactRequest = new();
        private string _searchString = "";
        private ClaimsPrincipal? _currentUser;

        private bool _canSearchContactRequests;
        private bool _canEditContactRequests;
        private bool _canDeleteContactRequests;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canSearchContactRequests = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.ContactRequests.Search)).Succeeded;
            _canEditContactRequests = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.ContactRequests.Edit)).Succeeded;
            _canDeleteContactRequests = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.ContactRequests.Delete)).Succeeded;

            await GetDataAsync();
            _loaded = true;
        }

        private async Task Reset()
        {
            await GetDataAsync();
        }

        private async Task GetDataAsync()
        {
            var response = await ContactRequestManager?.GetAllContactRequestsAsync()!;
            if (response.Succeeded)
            {
                _contactRequests = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool Search(GetAllContactRequestsResponse response)
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
            if (response.Message.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            if (response.Notes != null && response.Notes.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
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
                _contactRequest = _contactRequests.FirstOrDefault(c => c.Id == id);
                if (_contactRequest != null)
                {
                    parameters.Add(nameof(AddEditContactRequestModal.AddEditContactRequestModel), new AddEditContactRequestCommand
                    {
                        Id = _contactRequest.Id,
                        Name = _contactRequest.Name,
                        Email = _contactRequest.Email,
                        Contacted = _contactRequest.Contacted
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditContactRequestModal>(id == 0 ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Delete(int id)
        {
            var deleteContent = "Delete Contact Request";
            var parameters = new DialogParameters
            {
                { nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id) }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await ContactRequestManager.DeleteAsync(id);
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
