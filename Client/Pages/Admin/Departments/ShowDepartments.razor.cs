using System.Security.Claims;
using Application.Features.Departments.Commands.AddEdit;
using Application.Features.Departments.Queries.GetAll;
using Client.Infrastructure.Managers.Department;
using Common.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Admin.Departments
{
    public partial class ShowDepartments
    {
        private bool _loaded;
        [Inject] private IDepartmentManager? DepartmentManager { get; set; }

        public List<GetAllDepartmentsResponse> _departments = new();

        private GetAllDepartmentsResponse _department = new();
        private string _searchString = "";
        private ClaimsPrincipal? _currentUser;

        private bool _canSearchDepartments;
        private bool _canEditDepartments;
        private bool _canDeleteDepartments;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canSearchDepartments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Departments.Search)).Succeeded;
            _canEditDepartments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Departments.Edit)).Succeeded;
            _canDeleteDepartments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Departments.Delete)).Succeeded;

            await GetDataAsync();
            _loaded = true;
        }

        private async Task Reset()
        {
            await GetDataAsync();
        }

        private async Task GetDataAsync()
        {
            var response = await DepartmentManager?.GetAllAsync()!;
            if (response.Succeeded)
            {
                _departments = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool Search(GetAllDepartmentsResponse response)
        {
            var result = string.IsNullOrWhiteSpace(_searchString);

            if (result) return result;
            if (response.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
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
                _department = _departments.FirstOrDefault(c => c.Id == id);
                if (_department != null)
                {
                    parameters.Add(nameof(AddEditDepartmentModal.AddEditDepartmentModel), new AddEditDepartmentCommand
                    {
                        Id = _department.Id,
                        Name = _department.Name,
                        Description = _department.Description
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditDepartmentModal>(id == 0 ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Delete(int id)
        {
            var deleteContent = "Delete Department";
            var parameters = new DialogParameters
            {
                { nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id) }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await DepartmentManager.DeleteAsync(id);
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
