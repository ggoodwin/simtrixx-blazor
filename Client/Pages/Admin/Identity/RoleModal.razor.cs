﻿using Application.Requests.Identity;
using Blazored.FluentValidation;
using Client.Infrastructure.Managers.Identity.Roles;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Admin.Identity
{
    public partial class RoleModal
    {
        [Inject] private IRoleManager? RoleManager { get; set; }

        [Parameter] public RoleRequest RoleModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance? MudDialog { get; set; }

        private FluentValidationValidator? _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator != null && _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog?.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await RoleManager?.SaveAsync(RoleModel)!;
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
