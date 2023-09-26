using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using Domain.Enums;
using MediatR;

namespace Application.Features.SupportTickets.Commands.AddEdit
{
    public partial class AddEditSupportTicketCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public SupportPriority Priority { get; set; }
        [Required]
        public int SupportDepartmentId { get; set; }
        
        public string SimtrixxUserId { get; set; }
        public SupportStatus Status { get; set; }
    }

    internal class AddEditSupportTicketCommandHandler : IRequestHandler<AddEditSupportTicketCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditSupportTicketCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(AddEditSupportTicketCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var supportTicket = _mapper.Map<SupportTicket>(command);
                await _unitOfWork.Repository<SupportTicket>().AddAsync(supportTicket);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken,
                    ApplicationConstants.Cache.GetAllSupportTicketsCacheKey);
                return await Result<int>.SuccessAsync(supportTicket.Id, "Support Ticket Saved");
            }
            else
            {
                var supportTicket = await _unitOfWork.Repository<SupportTicket>().GetByIdAsync(command.Id);
                if (supportTicket != null)
                {
                    supportTicket.Subject = command.Subject;
                    supportTicket.Description = command.Description;
                    supportTicket.Priority = command.Priority;
                    supportTicket.SupportDepartmentId = command.SupportDepartmentId;
                    supportTicket.SimtrixxUserId = command.SimtrixxUserId;
                    supportTicket.Status = command.Status;
                    await _unitOfWork.Repository<SupportTicket>().UpdateAsync(supportTicket);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllSupportTicketsCacheKey);
                    return await Result<int>.SuccessAsync(supportTicket.Id, "Support Ticket Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Support Ticket Not Found!");
                }
            }
        }
    }
}
