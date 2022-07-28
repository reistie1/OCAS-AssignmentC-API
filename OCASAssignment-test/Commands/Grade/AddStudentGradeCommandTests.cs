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
    public class AddStudentGradeCommandTests
    {
        private readonly Mock<IGradeRepository> _mockGradeRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SampleData _sampleData;
        private readonly GradeDto _mappedGradeDto;
        private readonly Grade _mappedGrade;

        public AddStudentGradeCommandTests()
        {
            Guid courseId = Guid.NewGuid();
            Guid StudentId = Guid.NewGuid();

            _mappedGradeDto = new GradeDto(){Id = Guid.NewGuid(), StudentId = StudentId, AlphabeticGrade = 'A', NumericGrade = 89, CourseId = courseId};
            _mappedGrade = new Grade(){Id = Guid.NewGuid(), StudentId = StudentId, AlphabeticGrade = 'A', NumericGrade = 89, CourseId = courseId};

            _sampleData = new SampleData();
            _mockGradeRepo = new Mock<IGradeRepository>();
            _mockGradeRepo.Setup(c => c.AddStudentGradeAsync(StudentId, _mappedGrade)).ReturnsAsync(_mappedGrade);
            
            
            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<Grade>(_mappedGradeDto)).Returns(_mappedGrade);
            _mockMapper.Setup(m => m.Map<GradeDto>(_mappedGrade)).Returns(_mappedGradeDto);
            
        }

        [Fact]
        public async Task AddStudentGradeCommand_ReturnsAddedCourseEntity()
        {
            var command = new AddStudentGradeCommand(_mappedGradeDto.StudentId, _mappedGradeDto);
            var handler = new AddStudentGradeCommandHandler(_mockMapper.Object, _mockGradeRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<GradeDto>>(result);
            Assert.NotNull(result);
            Assert.Same(_mappedGradeDto, result.Data);
        }

        [Fact]
        public async Task AddStudentGradeCommand_ValidatesProperties()
        {
            GradeDto gradeDto = new GradeDto();
            var command = new AddStudentGradeCommand(gradeDto.StudentId, gradeDto);
            var validator = new AddStudentGradeCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.Equal(5, result.Errors.Count);
        }
    }
}