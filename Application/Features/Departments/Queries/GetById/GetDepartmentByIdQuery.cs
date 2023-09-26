using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Wrapper;
using Domain.Entities.Support;
using MediatR;

namespace Application.Features.Departments.Queries.GetById
{
    public class GetDepartmentByIdQuery : IRequest<Result<GetDepartmentByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Result<GetDepartmentByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetDepartmentByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery query, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Repository<SupportDepartment>().GetByIdAsync(query.Id);
            var mappedDepartment = _mapper.Map<GetDepartmentByIdResponse>(department);
            return await Result<GetDepartmentByIdResponse>.SuccessAsync(mappedDepartment);
        }
    }
}
