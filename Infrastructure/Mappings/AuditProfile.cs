using Application.Responses.Audit;
using AutoMapper;
using Domain.Entities.Audit;

namespace Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}
