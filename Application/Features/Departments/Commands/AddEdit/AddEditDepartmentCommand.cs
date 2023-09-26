using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using MediatR;

namespace Application.Features.Departments.Commands.AddEdit
{
    public partial class AddEditDepartmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    internal class AddEditDepartmentCommandHandler : IRequestHandler<AddEditDepartmentCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditDepartmentCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(AddEditDepartmentCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var department = _mapper.Map<SupportDepartment>(command);
                await _unitOfWork.Repository<SupportDepartment>().AddAsync(department);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken,
                    ApplicationConstants.Cache.GetAllDepartmentsCacheKey);
                return await Result<int>.SuccessAsync(department.Id, "Department Saved");
            }
            else
            {
                var department = await _unitOfWork.Repository<SupportDepartment>().GetByIdAsync(command.Id);
                if (department != null)
                {
                    department.Name = command.Name;
                    department.Description = command.Description;
                    await _unitOfWork.Repository<SupportDepartment>().UpdateAsync(department);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDepartmentsCacheKey);
                    return await Result<int>.SuccessAsync(department.Id, "Department Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Department Not Found!");
                }
            }
        }
    }
}
