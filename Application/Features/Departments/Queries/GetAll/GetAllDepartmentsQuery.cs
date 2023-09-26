using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Departments.Queries.GetAll
{
    public class GetAllDepartmentsQuery : IRequest<Result<List<GetAllDepartmentsResponse>>>
    {
        public GetAllDepartmentsQuery()
        {
        }
    }

    internal class GetAllDepartmentsCachedQueryHandler : IRequestHandler<GetAllDepartmentsQuery, Result<List<GetAllDepartmentsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllDepartmentsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllDepartmentsResponse>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<SupportDepartment, GetAllDepartmentsResponse>> expression = e => new GetAllDepartmentsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            };
            var getAllDepartments = async () => await _unitOfWork.Repository<SupportDepartment>().Entities
                .Select(expression)
                .ToListAsync(cancellationToken);

            var departmentList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllDepartmentsCacheKey, getAllDepartments);
            var mappedDepartments = _mapper.Map<List<GetAllDepartmentsResponse>>(departmentList);
            return await Result<List<GetAllDepartmentsResponse>>.SuccessAsync(mappedDepartments);
        }
    }
}
