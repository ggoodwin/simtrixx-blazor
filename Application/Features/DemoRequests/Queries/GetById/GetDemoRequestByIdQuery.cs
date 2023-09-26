using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.DemoRequests.Queries.GetById
{
    public class GetDemoRequestByIdQuery : IRequest<Result<GetDemoRequestByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetDemoRequestByIdQueryHandler : IRequestHandler<GetDemoRequestByIdQuery, Result<GetDemoRequestByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetDemoRequestByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetDemoRequestByIdResponse>> Handle(GetDemoRequestByIdQuery query, CancellationToken cancellationToken)
        {
            var demoRequest = await _unitOfWork.Repository<DemoRequest>().GetByIdAsync(query.Id);
            var mappedDemoRequest = _mapper.Map<GetDemoRequestByIdResponse>(demoRequest);
            return await Result<GetDemoRequestByIdResponse>.SuccessAsync(mappedDemoRequest);
        }
    }
}
