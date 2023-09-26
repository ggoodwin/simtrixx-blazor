using System.Net.Http.Headers;
using Client.Extensions;
using Client.Infrastructure.Managers.Identity.Roles;
using Common.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace Client.Shared.Layout
{
    public partial class MainBody
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public EventCallback OnDarkModeToggle { get; set; }

        private bool _drawerOpen = true;
        [Inject] private IRoleManager? RoleManager { get; set; }

        private string? CurrentUserId { get; set; }
        private string? ImageDataUrl { get; set; }
        private string? FirstName { get; set; }
        private string? SecondName { get; set; }
        private string? Email { get; set; }
        private char FirstLetterOfName { get; set; }
        private string _menuBackground { get; set; }
        private int _menuElevation { get; set; }

        public async Task ToggleDarkMode()
        {
            await OnDarkModeToggle.InvokeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            if (_navigationManager.Uri == _config["Urls:Base"] || _navigationManager.Uri == _config["Urls:BaseWWW"])
            {
                _menuBackground = "background-color: transparent";
                _menuElevation = 0;
            }
            else
            {
                _menuBackground = "background-color: #1283D9";
                _menuElevation = 25;
            }

            _interceptor.RegisterEvent();

            //_hubConnection = _hubConnection.TryInitialize(_navigationManager, _localStorage, _config);
            //if (_hubConnection != null)
            //{
            //    await _hubConnection.StartAsync();
            //    //_hubConnection = _hubConnection.TryInitialize(_navigationManager, _localStorage, _config);
            //    //await _hubConnection.StartAsync();
            //    _hubConnection.On(ApplicationConstants.SignalR.ReceiveRegenerateTokens, async () =>
            //    {
            //        try
            //        {
            //            var token = await _authenticationManager.TryForceRefreshToken();
            //            if (!string.IsNullOrEmpty(token))
            //            {
            //                _snackBar.Add("Refreshed Token.", Severity.Success);
            //                _httpClient.DefaultRequestHeaders.Authorization =
            //                    new AuthenticationHeaderValue("Bearer", token);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //            _snackBar.Add("You are Logged Out.", Severity.Error);
            //            await _authenticationManager.Logout();
            //            _navigationManager.NavigateTo("/");
            //        }
            //    });
            //    _hubConnection.On<string, string>(ApplicationConstants.SignalR.LogoutUsersByRole,
            //        async (userId, roleId) =>
            //        {
            //            if (CurrentUserId != userId)
            //            {
            //                var rolesResponse = await RoleManager?.GetRolesAsync()!;
            //                if (rolesResponse.Succeeded)
            //                {
            //                    var role = rolesResponse.Data.FirstOrDefault(x => x.Id == roleId);
            //                    if (role != null)
            //                    {
            //                        if (CurrentUserId != null)
            //                        {
            //                            var currentUserRolesResponse =
            //                                await _userManager.GetRolesAsync(CurrentUserId);
            //                            if (currentUserRolesResponse.Succeeded &&
            //                                currentUserRolesResponse.Data.UserRoles.Any(
            //                                    x => x.RoleName == role.Name))
            //                            {
            //                                _snackBar.Add(
            //                                    "You are logged out because the Permissions of one of your Roles have been updated.",
            //                                    Severity.Error);
            //                                await _hubConnection.SendAsync(
            //                                    ApplicationConstants.SignalR.OnDisconnect,
            //                                    CurrentUserId);
            //                                await _authenticationManager.Logout();
            //                                _navigationManager.NavigateTo("/login");
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        });
            //    _hubConnection.On<string>(ApplicationConstants.SignalR.PingRequest,
            //        async (userName) =>
            //        {
            //            await _hubConnection.SendAsync(ApplicationConstants.SignalR.PingResponse, CurrentUserId,
            //                userName);
            //        });

            //    await _hubConnection.SendAsync(ApplicationConstants.SignalR.OnConnect, CurrentUserId);

            //}
            if (!string.IsNullOrEmpty(FirstName))
            {
                _snackBar.Add($"Welcome {FirstName}", Severity.Success);
            }
        }

        protected override void OnInitialized()
        {
            if (_navigationManager.Uri == _config["Urls:Base"] || _navigationManager.Uri == _config["Urls:BaseWWW"])
            {
                _drawerOpen = false;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if (user == null) return;
            if (user.Identity?.IsAuthenticated == true)
            {
                if (string.IsNullOrEmpty(CurrentUserId))
                {
                    CurrentUserId = user.GetUserId();
                    FirstName = user.GetFirstName();
                    if (FirstName.Length > 0)
                    {
                        FirstLetterOfName = FirstName[0];
                    }

                    SecondName = user.GetLastName();
                    Email = user.GetEmail();
                    var imageResponse = await _accountManager.GetProfilePictureAsync(CurrentUserId);
                    if (imageResponse.Succeeded)
                    {
                        ImageDataUrl = imageResponse.Data;
                    }

                    var currentUserResult = await _userManager.GetAsync(CurrentUserId);
                    if (!currentUserResult.Succeeded || currentUserResult.Data == null)
                    {
                        _snackBar.Add(
                            "You are logged out because the user with your Token has been deleted.",
                            Severity.Error);
                        CurrentUserId = string.Empty;
                        ImageDataUrl = string.Empty;
                        FirstName = string.Empty;
                        SecondName = string.Empty;
                        Email = string.Empty;
                        FirstLetterOfName = char.MinValue;
                        await _authenticationManager.Logout();
                    }
                }
            }
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private void Logout()
        {
            var parameters = new DialogParameters
            {
                {nameof(Dialogs.Logout.ContentText), "Logout Confirmation"},
                {nameof(Dialogs.Logout.ButtonText), "Logout"},
                {nameof(Dialogs.Logout.Color), Color.Error},
                {nameof(Dialogs.Logout.CurrentUserId), CurrentUserId}//,
                //{nameof(Dialogs.Logout.HubConnection), _hubConnection}
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            _dialogService.Show<Dialogs.Logout>("Logout", parameters, options);
        }

        //private HubConnection? _hubConnection;
        //public bool IsConnected => _hubConnection is { State: HubConnectionState.Connected };
    }
}
