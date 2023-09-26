using Application.Features.Departments.Queries.GetAll;
using Application.Features.SupportTickets.Commands.AddEdit;
using Blazored.FluentValidation;
using Client.Extensions;
using Client.Infrastructure.Managers.Department;
using Client.Infrastructure.Managers.SupportTicket;
using Domain.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Support
{
    public partial class AddEditSupportTicketModal
    {
        [Inject] private ISupportTicketManager? SupportTicketManager { get; set; }
        [Inject] private IDepartmentManager? DepartmentManager { get; set; }

        [Parameter] public AddEditSupportTicketCommand AddEditSupportTicketModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance? MudDialog { get; set; }

        private FluentValidationValidator? _fluentValidationValidator;
        private bool _disabled;
        private List<GetAllDepartmentsResponse> _departments = new();
        private string UserId { get; set; }
        private SupportPriority EnumValue { get; set; } = SupportPriority.Low;

        protected override async Task OnInitializedAsync()
        {
            await LoadUser();
            await LoadDepartmentsAsync();
        }

        private async Task LoadUser()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            UserId = user.GetUserId();
        }

        private async Task LoadDepartmentsAsync()
        {
            var data = await DepartmentManager.GetAllAsync();
            if (data.Succeeded)
            {
                _departments = data.Data.ToList();
            }
            await Task.CompletedTask;
        }

        public void Cancel()
        {
            MudDialog?.Cancel();
        }

        private async Task SaveAsync()
        {
            _disabled = true;
            AddEditSupportTicketModel.Status = SupportStatus.Open;
            AddEditSupportTicketModel.SimtrixxUserId = UserId;
            AddEditSupportTicketModel.Priority = EnumValue;
            var response = await SupportTicketManager.SaveAsync(AddEditSupportTicketModel);
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

        private async Task<IEnumerable<int>> SearchDepartments(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _departments.Select(x => x.Id);

            return _departments.Where(x => x.Name == value)
                .Select(x => x.Id);
        }
    }
}
