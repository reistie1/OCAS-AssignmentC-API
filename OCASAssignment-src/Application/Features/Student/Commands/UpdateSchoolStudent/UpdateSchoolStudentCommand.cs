using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class UpdateSchoolStudentCommand : IRequest<Response<StudentDto>>
    {
        public UpdateSchoolStudentCommand(StudentDto student)
        {
            Student = student;
        }

        public StudentDto Student{ get; }
    }
}