using System.Data;
using Application.Features.Licenses.Commands.AddEdit;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Documents;
using Application.Requests;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Licenses.Commands.Import
{
    public partial class ImportLicensesCommand : IRequest<Result<int>>
    {
        public UploadRequest UploadRequest { get; set; }
    }

    internal class ImportLicensesCommandHandler : IRequestHandler<ImportLicensesCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IExcelService _excelService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddEditLicenseCommand> _addLicenseValidator;

        public ImportLicensesCommandHandler(
            IUnitOfWork<int> unitOfWork,
            IExcelService excelService,
            IMapper mapper,
            IValidator<AddEditLicenseCommand> addLicenseValidator)
        {
            _unitOfWork = unitOfWork;
            _excelService = excelService;
            _mapper = mapper;
            _addLicenseValidator = addLicenseValidator;
        }

        public async Task<Result<int>> Handle(ImportLicensesCommand request, CancellationToken cancellationToken)
        {
            var stream = new MemoryStream(request.UploadRequest.Data);
            var result = (await _excelService.ImportAsync(stream, mappers: new Dictionary<string, Func<DataRow, License, object>>
            {
                { "SimtrixxUserId", (row,item) => item.Key = row["SimtrixxUserId"].ToString() },
                { "Key", (row,item) => item.Key = row["Key"].ToString() },
                { "Expiration", (row,item) => item.Expiration = Convert.ToDateTime(row["Expiration"]) }
            }, "Licenses"));

            if (result.Succeeded)
            {
                var importedLicenses = result.Data;
                var errors = new List<string>();
                var errorsOccurred = false;
                foreach (var license in importedLicenses)
                {
                    var validationResult = await _addLicenseValidator.ValidateAsync(_mapper.Map<AddEditLicenseCommand>(license), cancellationToken);
                    if (validationResult.IsValid)
                    {
                        await _unitOfWork.Repository<License>().AddAsync(license);
                    }
                    else
                    {
                        errorsOccurred = true;
                        errors.AddRange(validationResult.Errors.Select(e => $"{(!string.IsNullOrWhiteSpace(license.Key) ? $"{license.Key} - " : string.Empty)}{e.ErrorMessage}"));
                    }
                }

                if (errorsOccurred)
                {
                    return await Result<int>.FailAsync(errors);
                }

                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllLicensesCacheKey);
                return await Result<int>.SuccessAsync(result.Data.Count(), result.Messages[0]);
            }
            else
            {
                return await Result<int>.FailAsync(result.Messages);
            }
        }
    }
}
