using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Wrapper;
using Domain.Entities.Support;
using MediatR;

namespace Application.Features.SupportTickets.Queries.GetById
{
    public class GetSupportReplyByIdQuery : IRequest<Result<GetSupportReplyByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetSupportTicketByIdQueryHandler : IRequestHandler<GetSupportReplyByIdQuery, Result<GetSupportReplyByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetSupportTicketByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetSupportReplyByIdResponse>> Handle(GetSupportReplyByIdQuery query, CancellationToken cancellationToken)
        {
            var supportTicket = await _unitOfWork.Repository<SupportTicket>().GetByIdAsync(query.Id);
            var mappedSupportTicket = _mapper.Map<GetSupportReplyByIdResponse>(supportTicket);
            return await Result<GetSupportReplyByIdResponse>.SuccessAsync(mappedSupportTicket);
        }
    }
}
