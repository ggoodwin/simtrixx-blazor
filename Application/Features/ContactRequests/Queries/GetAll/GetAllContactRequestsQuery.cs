using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ContactRequests.Queries.GetAll
{
    public class GetAllContactRequestsQuery : IRequest<Result<List<GetAllContactRequestsResponse>>>
    {
        public GetAllContactRequestsQuery()
        {
        }
    }

    internal class GetAllContactRequestsCachedQueryHandler : IRequestHandler<GetAllContactRequestsQuery, Result<List<GetAllContactRequestsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllContactRequestsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllContactRequestsResponse>>> Handle(GetAllContactRequestsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ContactRequest, GetAllContactRequestsResponse>> expression = e => new GetAllContactRequestsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Message = e.Message,
                Contacted = e.Contacted,
                Notes = e.Notes
            };
            var getAllContactRequests = async () => await _unitOfWork.Repository<ContactRequest>().Entities
                .Select(expression)
                .ToListAsync(cancellationToken);

            var contactRequestList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllContactRequestsCacheKey, getAllContactRequests);
            var mappedContactRequests = _mapper.Map<List<GetAllContactRequestsResponse>>(contactRequestList);
            return await Result<List<GetAllContactRequestsResponse>>.SuccessAsync(mappedContactRequests);
        }
    }
}
