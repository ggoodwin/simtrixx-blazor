using Application.Features.Stripe.StripeCustomers.Queries.GetByUser;
using Client.Extensions;
using Client.Infrastructure.Managers.Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Settings
{
    [Authorize]
    public partial class Billing
    {
        private bool _loaded;
        [Inject] private IStripeManager StripeManager { get; set; }
        private string Message { get; set; }
        private Severity Sev { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadBilling();
            _loaded = true;
        }

        private async Task LoadBilling()
        {
            //Get UserId
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            var userId = user.GetUserId();

            var userQuery = new GetStripeCustomerByUserQuery
            {
                UserId = userId
            };

            var customerResponse = await StripeManager.GetCustomerByUserAsync(userQuery);
            if (customerResponse.Succeeded && customerResponse.Data != null)
            {
                var billingResponse = await StripeManager.CreateBillingSessionAsync(customerResponse.Data.StripeCustomerId);
                _navigationManager.NavigateTo(billingResponse);
            }
            else
            {
                Sev = Severity.Error;
                Message = "No Billing Account Found";
            }
        }
    }
}
