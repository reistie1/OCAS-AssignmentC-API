using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddSchoolStudentCommand : IRequest<Response<StudentDto>>
    {
        public AddSchoolStudentCommand(StudentDto student)
        {
            Student = student;
        }

        public StudentDto Student{ get; }
    }
}