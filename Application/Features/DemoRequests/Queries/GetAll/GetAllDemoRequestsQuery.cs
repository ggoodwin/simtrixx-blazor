using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DemoRequests.Queries.GetAll
{
    public class GetAllDemoRequestsQuery : IRequest<Result<List<GetAllDemoRequestsResponse>>>
    {
        public GetAllDemoRequestsQuery()
        {
        }
    }

    internal class GetAllDemoRequestsCachedQueryHandler : IRequestHandler<GetAllDemoRequestsQuery, Result<List<GetAllDemoRequestsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllDemoRequestsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllDemoRequestsResponse>>> Handle(GetAllDemoRequestsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<DemoRequest, GetAllDemoRequestsResponse>> expression = e => new GetAllDemoRequestsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Contacted = e.Contacted,
                Notes = e.Notes
            };
            var getAllDemoRequests = async () => await _unitOfWork.Repository<DemoRequest>().Entities
                .Select(expression)
                .ToListAsync(cancellationToken);

            var demoRequestList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllDemoRequestsCacheKey, getAllDemoRequests);
            var mappedDemoRequests = _mapper.Map<List<GetAllDemoRequestsResponse>>(demoRequestList);
            return await Result<List<GetAllDemoRequestsResponse>>.SuccessAsync(mappedDemoRequests);
        }
    }
}
