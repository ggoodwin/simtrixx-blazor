using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SupportTickets.Queries.GetAll
{
    public class GetAllSupportTicketsQuery : IRequest<Result<List<GetAllSupportTicketsResponse>>>
    {
        public GetAllSupportTicketsQuery()
        {
        }
    }

    internal class GetAllSupportTicketsCachedQueryHandler : IRequestHandler<GetAllSupportTicketsQuery, Result<List<GetAllSupportTicketsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllSupportTicketsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllSupportTicketsResponse>>> Handle(GetAllSupportTicketsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<SupportTicket, GetAllSupportTicketsResponse>> expression = e => new GetAllSupportTicketsResponse
            {
                Id = e.Id,
                Subject = e.Subject,
                Description = e.Description,
                Priority = e.Priority,
                Status = e.Status,
                DepartmentName = e.SupportDepartment.Name,
                SupportDepartmentId = e.SupportDepartmentId,
                UserName = e.SimtrixxUser.UserName,
                SimtrixxUserId = e.SimtrixxUserId
            };
            var getAllSupportTickets = async () => await _unitOfWork.Repository<SupportTicket>().Entities
                .Select(expression)
                .ToListAsync(cancellationToken);

            var supportTicketList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllSupportTicketsCacheKey, getAllSupportTickets);
            var mappedSupportTickets = _mapper.Map<List<GetAllSupportTicketsResponse>>(supportTicketList);
            return await Result<List<GetAllSupportTicketsResponse>>.SuccessAsync(mappedSupportTickets);
        }
    }
}
