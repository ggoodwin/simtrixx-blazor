using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using Domain.Enums;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SupportTickets.Queries.GetAllByStatus
{
    public class GetAllSupportTicketsByStatusQuery : IRequest<Result<List<GetAllSupportTicketsByStatusResponse>>>
    {
        public SupportStatus Status { get; set; }
    }

    internal class GetAllSupportTicketsByStatusCachedQueryHandler : IRequestHandler<GetAllSupportTicketsByStatusQuery, Result<List<GetAllSupportTicketsByStatusResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllSupportTicketsByStatusCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllSupportTicketsByStatusResponse>>> Handle(GetAllSupportTicketsByStatusQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<SupportTicket, GetAllSupportTicketsByStatusResponse>> expression = e => new GetAllSupportTicketsByStatusResponse
            {
                Id = e.Id,
                Subject = e.Subject,
                Description = e.Description,
                Priority = e.Priority,
                Status = e.Status,
                DepartmentName = e.SupportDepartment.Name,
                UserName = e.SimtrixxUser.UserName,
                SupportDepartmentId = e.SupportDepartmentId,
                SimtrixxUserId = e.SimtrixxUserId
            };
            var getAllSupportTickets = async () => await _unitOfWork.Repository<SupportTicket>().Entities
                .Select(expression)
                .Where(x => x.Status == request.Status)
                .ToListAsync(cancellationToken);

            var supportTicketList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllSupportTicketsByStatusCacheKey, getAllSupportTickets);
            var mappedSupportTickets = _mapper.Map<List<GetAllSupportTicketsByStatusResponse>>(supportTicketList);
            return await Result<List<GetAllSupportTicketsByStatusResponse>>.SuccessAsync(mappedSupportTickets);
        }
    }
}
