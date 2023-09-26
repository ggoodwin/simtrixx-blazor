using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Configurations;
using Application.Features.Stripe.StripeCustomers.Commands.AddEdit;
using Application.Features.Stripe.StripeCustomers.Queries.GetById;
using Application.Features.Stripe.StripeCustomers.Queries.GetByUser;
using Application.Features.Stripe.StripeOrders.Commands.AddEdit;
using Application.Features.Stripe.StripeOrders.Queries.GetByOrderId;
using Application.Features.Stripe.StripeSubscriptions.Commands.AddEdit;
using Application.Interfaces.Repositories;
using Application.Responses.Stripe;
using Client.Infrastructure.Extensions;
using Common.Wrapper;
using Microsoft.Extensions.Options;

namespace Client.Infrastructure.Managers.Stripe
{
    public class StripeManager : IStripeManager
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<UrlConfiguration> _config;

        public StripeManager(HttpClient httpClient, IOptions<UrlConfiguration> config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> CreateCheckoutSessionAsync()
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.StripeEndpoints.CreateCheckoutSession}");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateBillingSessionAsync(string customerId)
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.StripeEndpoints.CreateBillingSession(customerId)}");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<StripeCheckoutResponse> GetCheckoutSessionAsync(string sessionId)
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.StripeEndpoints.GetCheckoutSession(sessionId)}");
            var data = await response.Content.ReadAsStringAsync();
            var convertedData = JsonSerializer.Deserialize<StripeCheckoutResponse>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });

            return convertedData;
        }

        public async Task<IResult<int>> SaveCustomerAsync(AddEditStripeCustomerCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.StripeEndpoints.SaveCustomer}", request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> SaveSubscriptionAsync(AddEditStripeSubscriptionCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.StripeEndpoints.SaveSubscription}", request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> SaveOrderAsync(AddEditStripeOrderCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.StripeEndpoints.SaveOrder}", request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<GetStripeOrderByOrderIdResponse>> GetOrderByOrderId(GetStripeOrderByOrderIdQuery request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.StripeEndpoints.GetOrderByOrderId}", request);
            return await response.ToResult<GetStripeOrderByOrderIdResponse>();
        }

        public async Task<IResult<GetStripeCustomerByIdResponse>> GetLicenseByIdAsync(GetStripeCustomerByIdQuery request)
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.StripeEndpoints.GetCustomer(request.Id)}");
            return await response.ToResult<GetStripeCustomerByIdResponse>();
        }

        public async Task<IResult<GetStripeCustomerByUserResponse>> GetCustomerByUserAsync(GetStripeCustomerByUserQuery request)
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.StripeEndpoints.GetCustomerByUser(request.UserId)}");
            return await response.ToResult<GetStripeCustomerByUserResponse>();
        }
    }
}
