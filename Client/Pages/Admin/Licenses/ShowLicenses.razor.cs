using System.Security.Claims;
using Application.Features.Licenses.Commands.AddEdit;
using Application.Features.Licenses.Commands.Import;
using Application.Features.Licenses.Queries.GetAll;
using Application.Requests;
using Client.Infrastructure.Managers.License;
using Client.Shared.Components;
using Common.Constants.Application;
using Common.Constants.Permission;
using Common.Wrapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Client.Pages.Admin.Licenses
{
    public partial class ShowLicenses
    {
        private bool _loaded;
        [Inject] private ILicenseManager? LicenseManager { get; set; }

        public List<GetAllLicensesResponse> _licenses = new();

        private GetAllLicensesResponse _license = new();
        private string _searchString = "";
        private ClaimsPrincipal? _currentUser;

        private bool _canExportLicenses;
        private bool _canImportLicenses;
        private bool _canSearchLicenses;
        private bool _canCreateLicenses;
        private bool _canEditLicenses;
        private bool _canDeleteLicenses;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canExportLicenses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Licenses.Export)).Succeeded;
            _canImportLicenses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Licenses.Import)).Succeeded;
            _canSearchLicenses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Licenses.Search)).Succeeded;
            _canCreateLicenses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Licenses.Create)).Succeeded;
            _canEditLicenses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Licenses.Edit)).Succeeded;
            _canDeleteLicenses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Licenses.Delete)).Succeeded;

            await GetDataAsync();
            _loaded = true;
        }

        private async Task Reset()
        {
            await GetDataAsync();
        }

        private async Task GetDataAsync()
        {
            var response = await LicenseManager?.GetAllLicensesAsync()!;
            if (response.Succeeded)
            {
                _licenses = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool Search(GetAllLicensesResponse response)
        {
            var result = string.IsNullOrWhiteSpace(_searchString);

            // check Search String
            if (result) return result;
            if (response.FirstName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            if (response.LastName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            if (response.UserName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            if (response.Email.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            return result;
        }
        
        private async Task ExportToExcelAsync()
        {
            var response = await LicenseManager?.DownloadFileAsync(_searchString)!;
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(License).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? "Licenses exported"
                    : "Filtered Licenses exported", Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task ImportFromExcelAsync()
        {
            var parameters = new DialogParameters
            {
                { nameof(ImportExcelModal.ModelName), "" }
            };
            Func<UploadRequest, Task<IResult<int>>> importExcel = ImportExcel;
            parameters.Add(nameof(ImportExcelModal.OnSaved), importExcel);
            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
                DisableBackdropClick = true
            };
            var dialog = _dialogService.Show<ImportExcelModal>("Import", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task<IResult<int>> ImportExcel(UploadRequest uploadFile)
        {
            var request = new ImportLicensesCommand { UploadRequest = uploadFile };
            var result = await LicenseManager.ImportAsync(request);
            return result;
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _license = _licenses.FirstOrDefault(c => c.Id == id);
                if (_license != null)
                {
                    parameters.Add(nameof(AddEditLicenseModal.AddEditLicenseModel), new AddEditLicenseCommand
                    {
                        Id = _license.Id,
                        Key = _license.Key,
                        Expiration = _license.Expiration
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditLicenseModal>(id == 0 ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Delete(int id)
        {
            var deleteContent = "Delete License";
            var parameters = new DialogParameters
            {
                { nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id) }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await LicenseManager.DeleteAsync(id);
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
