using Application.Features.Licenses.Commands.AddEdit;
using Application.Features.Licenses.Queries.GetAll;
using Application.Features.Licenses.Queries.GetById;
using Application.Features.Licenses.Queries.GetByUser;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class LicenseProfile : Profile
    {
        public LicenseProfile()
        {
            CreateMap<AddEditLicenseCommand, License>().ReverseMap();
            CreateMap<GetLicenseByIdResponse, License>().ReverseMap();
            CreateMap<GetAllLicensesResponse, License>().ReverseMap();
            CreateMap<GetLicensesByUserResponse, License>().ReverseMap();
        }
    }
}
