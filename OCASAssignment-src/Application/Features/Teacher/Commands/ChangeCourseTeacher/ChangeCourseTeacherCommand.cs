using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class ChangeCourseTeacherCommand : IRequest<Response<TeacherDto>>
    {
        public ChangeCourseTeacherCommand(ChangeTeacherRequest teacherChange)
        {
            TeacherChange = teacherChange;
        }

        public ChangeTeacherRequest TeacherChange { get; }
    }
}