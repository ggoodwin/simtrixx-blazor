using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Licenses.Commands.AddEdit
{
    public partial class AddEditLicenseCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Key { get; set; }
        public DateTime? Expiration { get; set; }
        [Required]
        public string SimtrixxUserId { get; set; }
    }

    internal class AddEditLicenseCommandHandler : IRequestHandler<AddEditLicenseCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditLicenseCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(AddEditLicenseCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var license = _mapper.Map<License>(command);
                await _unitOfWork.Repository<License>().AddAsync(license);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllLicensesCacheKey);
                return await Result<int>.SuccessAsync(license.Id, "License Saved");
            }
            else
            {
                var license = await _unitOfWork.Repository<License>().GetByIdAsync(command.Id);
                if (license != null)
                {
                    license.SimtrixxUserId = command.SimtrixxUserId;
                    license.Expiration = (DateTime)command.Expiration;
                    license.Key = command.Key;
                    await _unitOfWork.Repository<License>().UpdateAsync(license);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllLicensesCacheKey);
                    return await Result<int>.SuccessAsync(license.Id, "License Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("License Not Found!");
                }
            }
        }
    }
}
