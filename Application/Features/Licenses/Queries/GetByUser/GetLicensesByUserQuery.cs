using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Wrapper;
using MediatR;

namespace Application.Features.Licenses.Queries.GetByUser
{
    public class GetLicensesByUserQuery : IRequest<Result<List<GetLicensesByUserResponse>>>
    {
        public int UserId { get; set; }
    }

    internal class GetLicensesByUserQueryHandler : IRequestHandler<GetLicensesByUserQuery, Result<List<GetLicensesByUserResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly ILicenseRepository _licenseRepository;

        public GetLicensesByUserQueryHandler(IMapper mapper, ILicenseRepository license)
        {
            _mapper = mapper;
            _licenseRepository = license;
        }

        public async Task<Result<List<GetLicensesByUserResponse>>> Handle(GetLicensesByUserQuery query, CancellationToken cancellationToken)
        {
            var license = await _licenseRepository.GetByUserAsync();
            var mappedLicense = _mapper.Map<List<GetLicensesByUserResponse>>(license);
            return await Result<List<GetLicensesByUserResponse>>.SuccessAsync(mappedLicense);
        }
    }
}
