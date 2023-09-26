using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Application.Configurations;
using Application.Features.SupportReplys.Commands.AddEdit;
using Application.Features.SupportTickets.Queries.GetAllReplies;
using Application.Features.SupportTickets.Queries.GetReplyById;
using Client.Infrastructure.Extensions;
using Client.Infrastructure.Managers.SupportTicket;
using Common.Wrapper;
using Domain.Enums;
using Microsoft.Extensions.Options;

namespace Client.Infrastructure.Managers.SupportTicket
{
    public class SupportReplyManager : ISupportReplyManager
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<UrlConfiguration> _config;

        public SupportReplyManager(HttpClient httpClient, IOptions<UrlConfiguration> config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IResult<IEnumerable<GetAllSupportRepliesResponse>>> GetAllSupportRepliesAsync(int supportTicketId)
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.SupportReplyEndpoints.GetAll(supportTicketId)}");
            return await response.ToResult<IEnumerable<GetAllSupportRepliesResponse>>();
        }

        public async Task<IResult<GetSupportReplyByIdResponse>> GetSupportReplyByIdAsync(GetSupportReplyByIdQuery request)
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.SupportReplyEndpoints.GetById(request.Id)}");
            return await response.ToResult<GetSupportReplyByIdResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditSupportReplyCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.SupportReplyEndpoints.Save}", request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_config.Value.Api}/{Routes.SupportReplyEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }
    }
}
