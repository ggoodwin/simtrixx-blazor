using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Licenses.Queries.GetById
{
    public class GetLicenseByIdQuery : IRequest<Result<GetLicenseByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetLicenseByIdQueryHandler : IRequestHandler<GetLicenseByIdQuery, Result<GetLicenseByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetLicenseByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetLicenseByIdResponse>> Handle(GetLicenseByIdQuery query, CancellationToken cancellationToken)
        {
            var license = await _unitOfWork.Repository<License>().GetByIdAsync(query.Id);
            var mappedLicense = _mapper.Map<GetLicenseByIdResponse>(license);
            return await Result<GetLicenseByIdResponse>.SuccessAsync(mappedLicense);
        }
    }
}
