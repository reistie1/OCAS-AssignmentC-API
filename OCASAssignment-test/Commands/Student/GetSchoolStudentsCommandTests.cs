using System;
using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;

namespace OCASAPI_Tests.Commands
{
    public class GetSchoolStudentsCommandTests
    {
        private readonly Mock<IStudentRepository> _mockStudentRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly List<StudentDto> _mappedStudentDto;
        private readonly List<Student> _mappedStudent;

        public GetSchoolStudentsCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid StudentId = Guid.NewGuid();

            _mappedStudent = new List<Student>(){
                new Student(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId},
                new Student(){Id = Guid.NewGuid(), FirstName = "Other", LastName = "Student", SchoolId = SchoolId},
                new Student(){Id = Guid.NewGuid(), FirstName = "Another", LastName = "Student", SchoolId = SchoolId},
            };

            _mappedStudentDto = new List<StudentDto>(){
                new StudentDto(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId},
                new StudentDto(){Id = Guid.NewGuid(), FirstName = "Other", LastName = "Student", SchoolId = SchoolId},
                new StudentDto(){Id = Guid.NewGuid(), FirstName = "Another", LastName = "Student", SchoolId = SchoolId},
            };
            
            _mockStudentRepo = new Mock<IStudentRepository>();
            _mockStudentRepo.Setup(s => s.GetSchoolStudentsAsync(s => s.SchoolId == _mappedStudent.First().SchoolId)).ReturnsAsync(_mappedStudent);

            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<IReadOnlyList<StudentDto>>(It.IsAny<IReadOnlyList<Student>>())).Returns(_mappedStudentDto);
        }

        [Fact]
        public async Task GetSchoolStudentsCommand_ReturnsStudentListEntities()
        {
            var command = new GetSchoolStudentsCommand(_mappedStudentDto.First().SchoolId);
            var handler = new GetSchoolStudentsCommandHandler(_mockMapper.Object, _mockStudentRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<IReadOnlyList<StudentDto>>>(result);
            Assert.NotEmpty(result.Data);
            Assert.Same(_mappedStudentDto, result.Data);
        }
    }
}