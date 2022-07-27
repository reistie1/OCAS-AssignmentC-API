using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class UpdateSchoolTeacherCommand : IRequest<Response<TeacherDto>>
    {
        public UpdateSchoolTeacherCommand(TeacherDto teacher)
        {
            Teacher = teacher;
        }

        public TeacherDto Teacher {get; set;}
    }
}