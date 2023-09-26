using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Support;
using Domain.Enums;
using MediatR;

namespace Application.Features.SupportReplys.Commands.AddEdit
{
    public partial class AddEditSupportReplyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public int SupportTicketId { get; set; }
        public SupportStatus Status { get; set; }
        public SupportPriority Priority { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int SupportDepartmentId { get; set; }

        public string SimtrixxUserId { get; set; }
    }

    internal class AddEditSupportReplyCommandHandler : IRequestHandler<AddEditSupportReplyCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditSupportReplyCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(AddEditSupportReplyCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var supportReply = _mapper.Map<SupportReply>(command);
                await _unitOfWork.Repository<SupportReply>().AddAsync(supportReply);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken,
                    ApplicationConstants.Cache.GetAllSupportRepliesCacheKey);
                return await Result<int>.SuccessAsync(supportReply.Id, "Support Reply Ticket Saved");
            }
            else
            {
                var supportReply = await _unitOfWork.Repository<SupportReply>().GetByIdAsync(command.Id);
                if (supportReply != null)
                {
                    supportReply.Message = command.Message;
                    supportReply.SimtrixxUserId = command.SimtrixxUserId;
                    supportReply.SupportTicketId = command.SupportTicketId;
                    await _unitOfWork.Repository<SupportReply>().UpdateAsync(supportReply);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllSupportRepliesCacheKey);
                    return await Result<int>.SuccessAsync(supportReply.Id, "Support Reply Ticket Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Support Reply Ticket Not Found!");
                }
            }
        }
    }
}
