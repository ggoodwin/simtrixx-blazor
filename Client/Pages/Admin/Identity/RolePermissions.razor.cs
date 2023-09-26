using System.Security.Claims;
using Application.Requests.Identity;
using Application.Responses.Identity;
using AutoMapper;
using Client.Extensions;
using Client.Infrastructure.Managers.Identity.Roles;
using Client.Infrastructure.Mappings;
using Common.Constants.Application;
using Common.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace Client.Pages.Admin.Identity
{
    public partial class RolePermissions
    {
        [Inject] private IRoleManager? RoleManager { get; set; }

        [CascadingParameter] private HubConnection? HubConnection { get; set; }
        [Parameter] public string? Id { get; set; }
        [Parameter] public string? Title { get; set; }
        [Parameter] public string? Description { get; set; }

        private PermissionResponse? _model;
        private Dictionary<string, List<RoleClaimResponse>> GroupedRoleClaims { get; } = new();
        private IMapper? _mapper;
        private RoleClaimResponse _roleClaims = new();
        private RoleClaimResponse _selectedItem = new();
        private string _searchString = "";
        private const bool Dense = true;
        private const bool Striped = true;
        private const bool Bordered = true;

        private ClaimsPrincipal? _currentUser;
        private bool _canEditRolePermissions;
        private bool _canSearchRolePermissions;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canEditRolePermissions = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.RoleClaims.Edit)).Succeeded;
            _canSearchRolePermissions = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.RoleClaims.Search)).Succeeded;

            await GetRolePermissionsAsync();
            _loaded = true;
            HubConnection = HubConnection?.TryInitialize(_navigationManager, _localStorage, _config);
            if (HubConnection?.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetRolePermissionsAsync()
        {
            _mapper = new MapperConfiguration(c => { c.AddProfile<RoleProfile>(); }).CreateMapper();
            var roleId = Id;
            var result = await RoleManager?.GetPermissionsAsync(roleId)!;
            if (result.Succeeded)
            {
                _model = result.Data;
                GroupedRoleClaims.Add("All Permissions", _model.RoleClaims);
                foreach (var claim in _model.RoleClaims)
                {
                    if (GroupedRoleClaims.ContainsKey(claim.Group))
                    {
                        GroupedRoleClaims[claim.Group].Add(claim);
                    }
                    else
                    {
                        GroupedRoleClaims.Add(claim.Group, new List<RoleClaimResponse> { claim });
                    }
                }
                if (_model != null)
                {
                    Description = $"Manage {_model.RoleId} {_model.RoleName}'s Permissions";
                }
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    _snackBar.Add(error, Severity.Error);
                }
                _navigationManager.NavigateTo("/admin/identity/roles");
            }
        }

        private async Task SaveAsync()
        {
            if (_model != null)
            {
                var request = _mapper?.Map<PermissionResponse, PermissionRequest>(_model);
                var result = await RoleManager?.UpdatePermissionsAsync(request)!;
                if (result.Succeeded)
                {
                    _snackBar.Add(result.Messages[0], Severity.Success);
                    await HubConnection?.SendAsync(ApplicationConstants.SignalR.SendRegenerateTokens)!;
                    await HubConnection?.SendAsync(ApplicationConstants.SignalR.OnChangeRolePermissions, _currentUser.GetUserId(), request?.RoleId)!;
                    _navigationManager.NavigateTo("/admin/identity/roles");
                }
                else
                {
                    foreach (var error in result.Messages)
                    {
                        _snackBar.Add(error, Severity.Error);
                    }
                }
            }
        }

        private bool Search(RoleClaimResponse roleClaims)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (roleClaims.Value?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return roleClaims.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true;
        }

        private static Color GetGroupBadgeColor(int selected, int all)
        {
            if (selected == 0)
                return Color.Error;

            return selected == all ? Color.Success : Color.Info;
        }
    }
}
