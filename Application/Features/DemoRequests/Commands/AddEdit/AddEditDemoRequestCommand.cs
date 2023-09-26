using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Messaging;
using Application.Requests.Messaging;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.DemoRequests.Commands.AddEdit
{
    public partial class AddEditDemoRequestCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Notes { get; set; }
        public bool Contacted { get; set; }
    }

    internal class AddEditDemoRequestCommandHandler : IRequestHandler<AddEditDemoRequestCommand, Result<int>>
    {
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditDemoRequestCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<Result<int>> Handle(AddEditDemoRequestCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                //Send Email
                var mailRequest = new MailRequest
                {
                    To = "support@simtrixx.com",
                    Subject = "Demo Request",
                    Body = $"A demo has been requested by {command.Name} at {command.Email}",
                    ToName = "Simtrixx Support",
                    HtmlBody = $"A demo has been requested by {command.Name} at {command.Email}"
                };
                await _mailService.SendAsync(mailRequest);

                //Add to Database
                var demoRequest = _mapper.Map<DemoRequest>(command);
                await _unitOfWork.Repository<DemoRequest>().AddAsync(demoRequest);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken,
                    ApplicationConstants.Cache.GetAllDemoRequestsCacheKey);
                return await Result<int>.SuccessAsync(demoRequest.Id, "Demo Request Saved");
            }
            else
            {
                var demoRequest = await _unitOfWork.Repository<DemoRequest>().GetByIdAsync(command.Id);
                if (demoRequest != null)
                {
                    demoRequest.Email = command.Email;
                    demoRequest.Contacted = command.Contacted;
                    demoRequest.Name = command.Name;
                    demoRequest.Notes = command.Notes;
                    await _unitOfWork.Repository<DemoRequest>().UpdateAsync(demoRequest);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDemoRequestsCacheKey);
                    return await Result<int>.SuccessAsync(demoRequest.Id, "Demo Request Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Demo Request Not Found!");
                }
            }
        }
    }
}
