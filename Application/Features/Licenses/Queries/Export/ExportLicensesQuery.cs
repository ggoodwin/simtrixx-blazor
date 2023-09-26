using Application.Extensions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Documents;
using Application.Specifications.Licenses;
using Common.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Licenses.Queries.Export
{
    public class ExportLicensesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportLicensesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportLicensesQueryHandler : IRequestHandler<ExportLicensesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;

        public ExportLicensesQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ExportLicensesQuery request, CancellationToken cancellationToken)
        {
            var stampFilterSpec = new LicenseFilterSpecification(request.SearchString);
            var stamps = await _unitOfWork.Repository<License>().Entities
                .Specify(stampFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(stamps, mappers: new Dictionary<string, Func<License, object>>
            {
                { "Id", item => item.Id },
                { "Key", item => item.Key },
                { "Expiration", item => item.Expiration },
                { "SimtrixxUserId", item => item.SimtrixxUser.Id }
            }, sheetName: "Licenses");

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
