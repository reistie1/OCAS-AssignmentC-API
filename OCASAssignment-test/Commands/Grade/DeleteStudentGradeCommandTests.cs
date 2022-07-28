using Moq;
using AutoMapper;
using OCASAPI.Data;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.Validators;

namespace OCASAPI_Tests.Commands
{
    public class DeleteStudentGradeCommandTests
    {
        private readonly Mock<IGradeRepository> _mockGradeRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SampleData _sampleData;
        private readonly GradeDto _mappedGradeDto;
        private readonly Grade _mappedGrade;

        public DeleteStudentGradeCommandTests()
        {
            Guid courseId = Guid.NewGuid();
            Guid StudentId = Guid.NewGuid();

            _mappedGradeDto = new GradeDto(){Id = Guid.NewGuid(), StudentId = StudentId, AlphabeticGrade = 'A', NumericGrade = 89, CourseId = courseId};
            _mappedGrade = new Grade(){Id = Guid.NewGuid(), StudentId = StudentId, AlphabeticGrade = 'A', NumericGrade = 89, CourseId = courseId};

            _sampleData = new SampleData();
            _mockGradeRepo = new Mock<IGradeRepository>();
            _mockGradeRepo.Setup(c => c.DeleteStudentGradeAsync(StudentId, _mappedGrade.CourseId)).ReturnsAsync(true);
            
            
            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<GradeDto>(_mappedGrade)).Returns(_mappedGradeDto);            
        }

        [Fact]
        public async Task DeleteStudentGradeCommand_ReturnsAddedCourseEntity()
        {
            var command = new DeleteStudentGradeCommand(_mappedGradeDto.StudentId, _mappedGradeDto.CourseId);
            var handler = new DeleteStudentGradeCommandHandler(_mockMapper.Object, _mockGradeRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<bool>>(result);
            Assert.NotNull(result);
            Assert.True(result.Data);
        }
    }
}