using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SupportTickets.Queries.GetAllReplies
{
    public class GetAllSupportRepliesQuery : IRequest<Result<List<GetAllSupportRepliesResponse>>>
    {
        public int SupportTicketId { get; set; }
    }

    internal class GetAllSupportRepliesCachedQueryHandler : IRequestHandler<GetAllSupportRepliesQuery, Result<List<GetAllSupportRepliesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllSupportRepliesCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllSupportRepliesResponse>>> Handle(GetAllSupportRepliesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<SupportReply, GetAllSupportRepliesResponse>> expression = e => new GetAllSupportRepliesResponse
            {
                Id = e.Id,
                Message = e.Message,
                SimtrixxUserId = e.SimtrixxUserId,
                SupportTicketId = e.SupportTicketId,
                UserName = $"{e.SimtrixxUser.FirstName} {e.SimtrixxUser.LastName}"
            };
            var getAllSupportReplies = async () => await _unitOfWork.Repository<SupportReply>().Entities
                .Where(x => x.SupportTicketId == request.SupportTicketId)
                .Select(expression)
                .ToListAsync(cancellationToken);

            var supportReplyList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllSupportRepliesCacheKey, getAllSupportReplies);
            var mappedSupportReplies = _mapper.Map<List<GetAllSupportRepliesResponse>>(supportReplyList);
            return await Result<List<GetAllSupportRepliesResponse>>.SuccessAsync(mappedSupportReplies);
        }
    }
}
