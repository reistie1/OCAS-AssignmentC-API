using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;

namespace OCASAPI.Application.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Grade, GradeDto>().ReverseMap();


        }

    }
}