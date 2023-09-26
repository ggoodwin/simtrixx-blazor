using Application.Interfaces.Repositories;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Licenses.Commands.Delete
{
    public class DeleteLicenseCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteLicenseCommandHandler : IRequestHandler<DeleteLicenseCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteLicenseCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteLicenseCommand command, CancellationToken cancellationToken)
        {
            var stamp = await _unitOfWork.Repository<License>().GetByIdAsync(command.Id);
            if (stamp != null)
            {
                await _unitOfWork.Repository<License>().DeleteAsync(stamp);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllLicensesCacheKey);
                return await Result<int>.SuccessAsync(stamp.Id, "License Deleted");
            }
            else
            {
                return await Result<int>.FailAsync("Stamp Not Found!");
            }
        }
    }
}
