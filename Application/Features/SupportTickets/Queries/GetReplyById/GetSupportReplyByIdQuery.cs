using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Wrapper;
using Domain.Entities.Support;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SupportTickets.Queries.GetReplyById
{
    public class GetSupportReplyByIdQuery : IRequest<Result<GetSupportReplyByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetSupportReplyByIdQueryHandler : IRequestHandler<GetSupportReplyByIdQuery, Result<GetSupportReplyByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetSupportReplyByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetSupportReplyByIdResponse>> Handle(GetSupportReplyByIdQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<SupportReply, GetSupportReplyByIdResponse>> expression = e => new GetSupportReplyByIdResponse
            {
                Id = e.Id,
                Message = e.Message,
                SimtrixxUserId = e.SimtrixxUserId,
                SupportTicketId = e.SupportTicketId,
                UserName = $"{e.SimtrixxUser.FirstName} {e.SimtrixxUser.LastName}"
            };

            var supportReply = async () => await _unitOfWork.Repository<SupportReply>().Entities
                .Where(x => x.Id == query.Id)
                .Select(expression)
                .FirstOrDefaultAsync(cancellationToken);

            var mappedSupportReply = _mapper.Map<GetSupportReplyByIdResponse>(supportReply);
            return await Result<GetSupportReplyByIdResponse>.SuccessAsync(mappedSupportReply);
        }
    }
}
