using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContactRequests.Queries.GetById
{
    public class GetContactRequestByIdQuery : IRequest<Result<GetContactRequestByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetContactRequestByIdQueryHandler : IRequestHandler<GetContactRequestByIdQuery, Result<GetContactRequestByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetContactRequestByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetContactRequestByIdResponse>> Handle(GetContactRequestByIdQuery query, CancellationToken cancellationToken)
        {
            var contactRequest = await _unitOfWork.Repository<ContactRequest>().GetByIdAsync(query.Id);
            var mappedContactRequest = _mapper.Map<GetContactRequestByIdResponse>(contactRequest);
            return await Result<GetContactRequestByIdResponse>.SuccessAsync(mappedContactRequest);
        }
    }
}
