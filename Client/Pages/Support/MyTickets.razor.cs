using System.Security.Claims;
using Application.Features.SupportTickets.Commands.AddEdit;
using Application.Features.SupportTickets.Queries.GetAll;
using Application.Features.SupportTickets.Queries.GetAllByUserId;
using Client.Extensions;
using Client.Infrastructure.Managers.SupportTicket;
using Common.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Support
{
    [Authorize]
    public partial class MyTickets
    {
        private bool _loaded;
        [Inject] private ISupportTicketManager? SupportTicketManager { get; set; }

        public List<GetAllSupportTicketsResponse> _supportTickets = new();

        private GetAllSupportTicketsResponse _supportTicket = new();
        private string _searchString = "";
        private ClaimsPrincipal? _currentUser;
        private bool ShowDetails { get; set; } = false;

        private bool _canSearchSupportTickets;
        private bool _canEditSupportTickets;
        private bool _canDeleteSupportTickets;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canSearchSupportTickets = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.SupportTickets.Search)).Succeeded;
            _canEditSupportTickets = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.SupportTickets.Edit)).Succeeded;
            _canDeleteSupportTickets = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.SupportTickets.Delete)).Succeeded;

            await GetDataAsync();
            _loaded = true;
        }

        private async Task Reset()
        {
            await GetDataAsync();
        }

        private void ShowBtnPress()
        {
            ShowDetails = !ShowDetails;
        }

        private async Task GetDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            var userId = user.GetUserId();
            var response = await SupportTicketManager?.GetAllSupportTicketsByUserAsync(userId)!;
            if (response.Succeeded)
            {
                _supportTickets = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool Search(GetAllSupportTicketsResponse response)
        {
            var result = string.IsNullOrWhiteSpace(_searchString);

            if (result) return result;
            if (response.Subject.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            if (response.Description.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
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
                _supportTicket = _supportTickets.FirstOrDefault(c => c.Id == id);
                if (_supportTicket != null)
                {
                    parameters.Add(nameof(AddEditSupportTicketModal.AddEditSupportTicketModel), new AddEditSupportTicketCommand
                    {
                        Id = _supportTicket.Id,
                        Subject = _supportTicket.Subject,
                        Description = _supportTicket.Description,
                        Status = _supportTicket.Status,
                        Priority = _supportTicket.Priority,
                        SimtrixxUserId = _supportTicket.SimtrixxUserId,
                        SupportDepartmentId = _supportTicket.SupportDepartmentId
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditSupportTicketModal>(id == 0 ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Delete(int id)
        {
            var deleteContent = "Delete Support Ticket";
            var parameters = new DialogParameters
            {
                { nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id) }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await SupportTicketManager.DeleteAsync(id);
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
