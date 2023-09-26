using Application.Interfaces.Repositories;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using MediatR;

namespace Application.Features.Departments.Commands.Delete
{
    public class DeleteDepartmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteDepartmentCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Repository<SupportDepartment>().GetByIdAsync(command.Id);
            if (department != null)
            {
                await _unitOfWork.Repository<SupportDepartment>().DeleteAsync(department);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDepartmentsCacheKey);
                return await Result<int>.SuccessAsync(department.Id, "Department Deleted");
            }
            else
            {
                return await Result<int>.FailAsync("Department Not Found!");
            }
        }
    }
}
