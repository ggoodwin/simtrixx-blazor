using Application.Interfaces.Repositories;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContactRequests.Commands.Delete
{
    public class DeleteContactRequestCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteContactRequestCommandHandler : IRequestHandler<DeleteContactRequestCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteContactRequestCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteContactRequestCommand command, CancellationToken cancellationToken)
        {
            var contactRequest = await _unitOfWork.Repository<ContactRequest>().GetByIdAsync(command.Id);
            if (contactRequest != null)
            {
                await _unitOfWork.Repository<ContactRequest>().DeleteAsync(contactRequest);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllContactRequestsCacheKey);
                return await Result<int>.SuccessAsync(contactRequest.Id, "Contact Request Deleted");
            }
            else
            {
                return await Result<int>.FailAsync("Contact Request Not Found!");
            }
        }
    }
}
