using Application.Interfaces.Repositories;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.DemoRequests.Commands.Delete
{
    public class DeleteDemoRequestCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteDemoRequestCommandHandler : IRequestHandler<DeleteDemoRequestCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteDemoRequestCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteDemoRequestCommand command, CancellationToken cancellationToken)
        {
            var demoRequest = await _unitOfWork.Repository<DemoRequest>().GetByIdAsync(command.Id);
            if (demoRequest != null)
            {
                await _unitOfWork.Repository<DemoRequest>().DeleteAsync(demoRequest);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDemoRequestsCacheKey);
                return await Result<int>.SuccessAsync(demoRequest.Id, "Demo Request Deleted");
            }
            else
            {
                return await Result<int>.FailAsync("Demo Request Not Found!");
            }
        }
    }
}
