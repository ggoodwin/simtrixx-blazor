using Application.Features.Licenses.Commands.AddEdit;
using Application.Features.Licenses.Queries.GetById;
using Application.Features.Stripe.StripeCustomers.Commands.AddEdit;
using Application.Features.Stripe.StripeOrders.Commands.AddEdit;
using Application.Features.Stripe.StripeOrders.Queries.GetByOrderId;
using Application.Features.Stripe.StripeSubscriptions.Commands.AddEdit;
using Application.Requests.Messaging;
using Client.Extensions;
using Client.Infrastructure.Managers.License;
using Client.Infrastructure.Managers.Messaging;
using Client.Infrastructure.Managers.Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Client.Pages.Checkout
{
    [Authorize]
    public partial class Success
    {
        [Parameter]
        public string TheId { get; set; }
        
        [Inject] private IStripeManager StripeManager { get; set; }
        [Inject] private ILicenseManager LicenseManager { get; set; }
        [Inject] private IEmailManager EmailManager { get; set; }

        private bool _loaded;
        private string _license;

        protected override async Task OnInitializedAsync()
        {
            await GenerateLicense();

            _loaded = true;
        }

        private async Task GenerateLicense()
        {
            //Get UserId
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            var userId = user.GetUserId();

            //Check if Order exists
            var stripeOrder = new GetStripeOrderByOrderIdQuery
            {
                OrderId = TheId
            };

            var orderResponse = await StripeManager.GetOrderByOrderId(stripeOrder);
            if (orderResponse.Succeeded)
            {
                var licenseQuery = new GetLicenseByIdQuery
                {
                    Id = orderResponse.Data.LicenseId
                };
                //Order already exists, just show the license key
                var licenseResults = await LicenseManager.GetLicenseByIdAsync(licenseQuery);
                _license = licenseResults.Data.Key;
            }
            else
            {
                //Get Stripe API Details
                var checkoutResponse = await StripeManager.GetCheckoutSessionAsync(TheId);
                if (checkoutResponse != null)
                {
                    //Insert StripeCustomer
                    var stripeCustomerCommand = new AddEditStripeCustomerCommand
                    {
                        StripeCustomerId = checkoutResponse.CustomerId,
                        SimtrixxUserId = userId
                    };
                    var customerResponse = await StripeManager.SaveCustomerAsync(stripeCustomerCommand);

                    //Insert StripeSubscription
                    var stripeSubscriptionCommand = new AddEditStripeSubscriptionCommand
                    {
                        StripeSubscriptionId = checkoutResponse.SubscriptionId,
                        SimtrixxUserId = userId
                    };
                    var subResponse = await StripeManager.SaveSubscriptionAsync(stripeSubscriptionCommand);

                    //Insert License
                    var licenseCommand = new AddEditLicenseCommand
                    {
                        Key = Guid.NewGuid().ToString(),
                        Expiration = DateTime.Now.AddYears(1),
                        SimtrixxUserId = userId
                    };
                    var licenseResponse = await LicenseManager.SaveAsync(licenseCommand);
                    _license = licenseCommand.Key;

                    //Insert StripeOrder
                    var orderCommands = new AddEditStripeOrderCommand
                    {
                        Email = checkoutResponse.CustomerDetails.Email,
                        SimtrixxUserId = userId,
                        LicenseId = licenseResponse.Data,
                        StripeCustomerId = customerResponse.Data,
                        StripeOrderId = TheId,
                        StripeSubscriptionId = subResponse.Data
                    };
                    var newOrderResponse = await StripeManager.SaveOrderAsync(orderCommands);


                    var thankYouEmail = new MailRequest()
                    {
                        To = checkoutResponse.CustomerDetails.Email,
                        Subject = @"Thank you for your purchase!",
                        ToName = $"{user.GetFirstName()} {user.GetLastName()}",
                        Body = @$"Thank you for your purchase of Simple Matrix.{Environment.NewLine}{Environment.NewLine}Registration Key: {_license}{Environment.NewLine}{Environment.NewLine}Download Link: https://simtrixx.blob.core.windows.net/install/SimTrixxSetup.exe{Environment.NewLine}{Environment.NewLine}Contact us here if you have any issues getting up and running.{Environment.NewLine}https://simtrixx.com/contact",
                        HtmlBody = @$"Thank you for your purchase of Simple Matrix.<br/><br/>Registration Key: {_license}<br/><br/><a href=""https://simtrixx.blob.core.windows.net/install/SimTrixxSetup.exe"" target=""_blank"">Download Link</a><br/><br/>Contact us <a href=""https://simtrixx.com/contact"" target=""_blank"">here</a> if you have any issues getting up and running."
                    };
                    await EmailManager.SendMailAsync(thankYouEmail);

                    var emailAddresses = new List<string>
                    {
                        "al@simtrixx.com",
                        "greg@simtrixx.com",
                        "brad@simtrixx.com"
                    };
                    var alEmail = new MultipleMailRequest()
                    {
                        To = emailAddresses,
                        Subject = "Simple Matrix Purchase",
                        ToName = "Al McCormick",
                        Body = @$"Great News!{Environment.NewLine}Someone just purchased a copy of Simple Matrix from the Simtrixx website.{Environment.NewLine}The customer's name is {user.GetFirstName()} {user.GetLastName()} and their email address is {checkoutResponse.CustomerDetails.Email}",
                        HtmlBody = @$"Great News!<br/>Someone just purchased a copy of Simple Matrix from the Simtrixx website.<br/>The customer's name is {user.GetFirstName()} {user.GetLastName()} and their email address is {checkoutResponse.CustomerDetails.Email}"
                    };

                    await EmailManager.SendMultipleMailAsync(alEmail);
                }
                else
                {
                    _snackBar.Add("Error Retrieving Order, please contact support.", Severity.Error);
                    _license = "Error";
                }
            }
        }

        private async Task CopyToClipboard()
        {
            await _jsRuntime.InvokeVoidAsync("clipboardCopy.copyText", _license);
            _snackBar.Add("Key Copied to Clipboard", Severity.Success);
        }
    }
}
