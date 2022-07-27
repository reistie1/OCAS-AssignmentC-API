using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddSchoolTeacherCommand : IRequest<Response<TeacherDto>>
    {
        public AddSchoolTeacherCommand(TeacherDto teacher)
        {
            Teacher = teacher;
        }

        public TeacherDto Teacher {get; set;}
    }
}