using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class ChangeCourseTeacherCommand : IRequest<Response<bool>>
    {
        public ChangeCourseTeacherCommand(ChangeTeacherRequest teacherChange)
        {
            TeacherChange = teacherChange;
        }

        public ChangeTeacherRequest TeacherChange { get; }
    }
}