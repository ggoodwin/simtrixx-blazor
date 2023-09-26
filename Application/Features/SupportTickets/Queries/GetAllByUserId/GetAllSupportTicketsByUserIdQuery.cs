using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SupportTickets.Queries.GetAllByUserId
{
    public class GetAllSupportTicketsByUserIdQuery : IRequest<Result<List<GetAllSupportTicketsByUserIdResponse>>>
    {
        public string UserId { get; set; }
    }

    internal class GetAllSupportTicketsByUserIdCachedQueryHandler : IRequestHandler<GetAllSupportTicketsByUserIdQuery, Result<List<GetAllSupportTicketsByUserIdResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllSupportTicketsByUserIdCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllSupportTicketsByUserIdResponse>>> Handle(GetAllSupportTicketsByUserIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<SupportTicket, GetAllSupportTicketsByUserIdResponse>> expression = e => new GetAllSupportTicketsByUserIdResponse
            {
                Id = e.Id,
                Subject = e.Subject,
                Description = e.Description,
                Priority = e.Priority,
                Status = e.Status,
                DepartmentName = e.SupportDepartment.Name,
                UserName = e.SimtrixxUser.UserName
            };
            var getAllSupportTickets = async () => await _unitOfWork.Repository<SupportTicket>().Entities
                .Where(x => x.SimtrixxUserId == request.UserId)
                .Select(expression)
                .ToListAsync(cancellationToken);

            var supportTicketList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllSupportTicketsByUserIdCacheKey, getAllSupportTickets);
            var mappedSupportTickets = _mapper.Map<List<GetAllSupportTicketsByUserIdResponse>>(supportTicketList);
            return await Result<List<GetAllSupportTicketsByUserIdResponse>>.SuccessAsync(mappedSupportTickets);
        }
    }
}
