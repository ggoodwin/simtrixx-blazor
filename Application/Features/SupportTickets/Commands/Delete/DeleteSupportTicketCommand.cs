using Application.Interfaces.Repositories;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using MediatR;

namespace Application.Features.SupportTickets.Commands.Delete
{
    public class DeleteSupportTicketCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteSupportTicketCommandHandler : IRequestHandler<DeleteSupportTicketCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteSupportTicketCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteSupportTicketCommand command, CancellationToken cancellationToken)
        {
            var supportTicket = await _unitOfWork.Repository<SupportTicket>().GetByIdAsync(command.Id);
            if (supportTicket != null)
            {
                await _unitOfWork.Repository<SupportTicket>().DeleteAsync(supportTicket);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllSupportTicketsCacheKey);
                return await Result<int>.SuccessAsync(supportTicket.Id, "Support Ticket Deleted");
            }
            else
            {
                return await Result<int>.FailAsync("Support Ticket Not Found!");
            }
        }
    }
}
