using Application.Interfaces.Repositories;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using MediatR;

namespace Application.Features.SupportTickets.Commands.Delete
{
    public class DeleteSupportReplyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteSupportReplyCommandHandler : IRequestHandler<DeleteSupportReplyCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteSupportReplyCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteSupportReplyCommand command, CancellationToken cancellationToken)
        {
            var supportReply = await _unitOfWork.Repository<SupportReply>().GetByIdAsync(command.Id);
            if (supportReply != null)
            {
                await _unitOfWork.Repository<SupportReply>().DeleteAsync(supportReply);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllSupportRepliesCacheKey);
                return await Result<int>.SuccessAsync(supportReply.Id, "Support Reply Deleted");
            }
            else
            {
                return await Result<int>.FailAsync("Support Reply Not Found!");
            }
        }
    }
}
