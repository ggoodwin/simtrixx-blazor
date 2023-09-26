using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Licenses.Queries.GetAll
{
    public class GetAllLicensesQuery : IRequest<Result<List<GetAllLicensesResponse>>>
    {
        public GetAllLicensesQuery()
        {
        }
    }

    internal class GetAllLicensesCachedQueryHandler : IRequestHandler<GetAllLicensesQuery, Result<List<GetAllLicensesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllLicensesCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllLicensesResponse>>> Handle(GetAllLicensesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<License, GetAllLicensesResponse>> expression = e => new GetAllLicensesResponse
            {
                Id = e.Id,
                Key = e.Key,
                Expiration = e.Expiration,
                SimtrixxUserId = e.SimtrixxUserId,
                FirstName = e.SimtrixxUser.FirstName,
                LastName = e.SimtrixxUser.LastName,
                UserName = e.SimtrixxUser.UserName,
                Email = e.SimtrixxUser.Email
            };
            var getAllLicenses = async () => await _unitOfWork.Repository<License>().Entities
                .Select(expression)
                .ToListAsync(cancellationToken);
            
            var licenseList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllLicensesCacheKey, getAllLicenses);
            var mappedLicenses = _mapper.Map<List<GetAllLicensesResponse>>(licenseList);
            return await Result<List<GetAllLicensesResponse>>.SuccessAsync(mappedLicenses);
        }
    }
}
