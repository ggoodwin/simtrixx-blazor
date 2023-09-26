using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Messaging;
using Application.Requests.Messaging;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContactRequests.Commands.AddEdit
{
    public partial class AddEditContactRequestCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Contacted { get; set; }
        public string? Notes { get; set; }
    }

    internal class AddEditContactRequestCommandHandler : IRequestHandler<AddEditContactRequestCommand, Result<int>>
    {
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditContactRequestCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<Result<int>> Handle(AddEditContactRequestCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                //Send Email
                var mailRequest = new MailRequest
                {
                    To = "support@simtrixx.com",
                    Subject = "Contact Request",
                    Body = $"A contact has been requested by {command.Name} at {command.Email} \n {command.Message}",
                    ToName = "Simtrixx Support",
                    HtmlBody = $"A demo has been requested by {command.Name} at {command.Email}<br/>{command.Message}"
                };
                await _mailService.SendAsync(mailRequest);

                //Add to Database
                var contactRequest = _mapper.Map<ContactRequest>(command);
                await _unitOfWork.Repository<ContactRequest>().AddAsync(contactRequest);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken,
                    ApplicationConstants.Cache.GetAllContactRequestsCacheKey);
                return await Result<int>.SuccessAsync(contactRequest.Id, "Contact Request Saved");
            }
            else
            {
                var contactRequest = await _unitOfWork.Repository<ContactRequest>().GetByIdAsync(command.Id);
                if (contactRequest != null)
                {
                    contactRequest.Email = command.Email;
                    contactRequest.Message = command.Message;
                    contactRequest.Contacted = command.Contacted;
                    contactRequest.Name = command.Name;
                    contactRequest.Notes = command.Notes;
                    await _unitOfWork.Repository<ContactRequest>().UpdateAsync(contactRequest);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllContactRequestsCacheKey);
                    return await Result<int>.SuccessAsync(contactRequest.Id, "Contact Request Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Contact Request Not Found!");
                }
            }
        }
    }
}
