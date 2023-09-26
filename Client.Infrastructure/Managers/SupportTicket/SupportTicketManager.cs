using System.Net.Http.Json;
using Application.Configurations;
using Application.Features.SupportTickets.Commands.AddEdit;
using Application.Features.SupportTickets.Queries.GetAll;
using Application.Features.SupportTickets.Queries.GetById;
using Client.Infrastructure.Extensions;
using Common.Wrapper;
using Domain.Enums;
using Microsoft.Extensions.Options;

namespace Client.Infrastructure.Managers.SupportTicket
{
    public class SupportTicketManager : ISupportTicketManager
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<UrlConfiguration> _config;

        public SupportTicketManager(HttpClient httpClient, IOptions<UrlConfiguration> config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IResult<IEnumerable<GetAllSupportTicketsResponse>>> GetAllSupportTicketsAsync()
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.SupportTicketEndpoints.GetAll}");
            return await response.ToResult<IEnumerable<GetAllSupportTicketsResponse>>();
        }

        public async Task<IResult<IEnumerable<GetAllSupportTicketsResponse>>> GetAllSupportTicketsByUserAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.SupportTicketEndpoints.GetByUser(userId)}");
            return await response.ToResult<IEnumerable<GetAllSupportTicketsResponse>>();
        }

        public async Task<IResult<IEnumerable<GetAllSupportTicketsResponse>>> GetAllSupportTicketsByStatusAsync(SupportStatus status)
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.SupportTicketEndpoints.GetByStatus(status)}");
            return await response.ToResult<IEnumerable<GetAllSupportTicketsResponse>>();
        }

        public async Task<IResult<GetSupportReplyByIdResponse>> GetSupportTicketByIdAsync(GetSupportReplyByIdQuery request)
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.SupportTicketEndpoints.GetById(request.Id)}");
            return await response.ToResult<GetSupportReplyByIdResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditSupportTicketCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.SupportTicketEndpoints.Save}", request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_config.Value.Api}/{Routes.SupportTicketEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }
    }
}
