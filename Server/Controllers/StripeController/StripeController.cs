using Application.Configurations;
using Application.Features.Stripe.StripeCustomers.Commands.AddEdit;
using Application.Features.Stripe.StripeCustomers.Queries.GetById;
using Application.Features.Stripe.StripeCustomers.Queries.GetByUser;
using Application.Features.Stripe.StripeOrders.Commands.AddEdit;
using Application.Features.Stripe.StripeOrders.Queries.GetByOrderId;
using Application.Features.Stripe.StripeSubscriptions.Commands.AddEdit;
using Application.Responses.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Server.Controllers.StripeController
{
    [Authorize]
    public class StripeController : BaseApiController<StripeController>
    {
        public readonly IOptions<StripeOptions> Options;
        private readonly IStripeClient _client;

        public StripeController(IOptions<StripeOptions> options)
        {
            Options = options;
            _client = new StripeClient(Options.Value.SecretKey);
        }

        [HttpGet("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{Options.Value.Domain}/success/{{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{Options.Value.Domain}/cancelled",
                Mode = "subscription",
                LineItems = new List<SessionLineItemOptions>
                {
                    new()
                    {
                        Price = Options.Value.ProPrice,
                        Quantity = 1
                    },
                }
            };
            var service = new SessionService(_client);

            var session = await service.CreateAsync(options);
            return Ok(session.Url);
        }

        [HttpGet("checkout-session/{sessionId}")]
        public async Task<IActionResult> CheckoutSession(string sessionId)
        {
            var service = new SessionService(_client);
            var session = await service.GetAsync(sessionId);
            return Ok(session);
        }

        [HttpGet("billing-portal/{customerId}")]
        public async Task<IActionResult> BillingPortalAsync(string customerId)
        {
            var returnUrl = this.Options.Value.Domain;

            var options = new Stripe.BillingPortal.SessionCreateOptions
            {
                Customer = customerId,
                ReturnUrl = returnUrl,
            };
            var service = new Stripe.BillingPortal.SessionService(_client);
            var session = await service.CreateAsync(options);

            return Ok(session.Url);
        }

        [HttpPost("save-customer")]
        public async Task<IActionResult> SaveCustomerAsync(AddEditStripeCustomerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("save-subscription")]
        public async Task<IActionResult> SaveCheckoutAsync(AddEditStripeSubscriptionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("save-order")]
        public async Task<IActionResult> SaveOrderAsync(AddEditStripeOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("get-by-order-id")]
        public async Task<IActionResult> GetByOrderIdAsync(GetStripeOrderByOrderIdQuery command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("get-customer/{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var license = await _mediator.Send(new GetStripeCustomerByIdQuery { Id = id });
            return Ok(license);
        }

        [HttpGet("get-customer-by-user/{userId}")]
        public async Task<IActionResult> GetCustomerByUserAsync(string userId)
        {
            var customers = await _mediator.Send(new GetStripeCustomerByUserQuery { UserId = userId });
            return Ok(customers);
        }
    }
}
