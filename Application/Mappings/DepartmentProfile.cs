using Application.Features.Departments.Commands.AddEdit;
using Application.Features.Departments.Queries.GetAll;
using Application.Features.Departments.Queries.GetById;
using AutoMapper;
using Domain.Entities.Support;

namespace Application.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<AddEditDepartmentCommand, SupportDepartment>().ReverseMap();
            CreateMap<GetDepartmentByIdResponse, SupportDepartment>().ReverseMap();
            CreateMap<GetAllDepartmentsResponse, SupportDepartment>().ReverseMap();
        }
    }
}
