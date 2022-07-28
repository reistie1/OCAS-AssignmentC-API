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
    public class UpdateStudentGradeCommandTests
    {
        private readonly Mock<IGradeRepository> _mockGradeRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SampleData _sampleData;
        private readonly GradeDto _mappedGradeDto;
        private readonly Grade _mappedGrade;

        public UpdateStudentGradeCommandTests()
        {
            Guid courseId = Guid.NewGuid();
            Guid StudentId = Guid.NewGuid();

            _mappedGradeDto = new GradeDto(){Id = Guid.NewGuid(), StudentId = StudentId, AlphabeticGrade = 'A', NumericGrade = 89, CourseId = courseId};
            _mappedGrade = new Grade(){Id = Guid.NewGuid(), StudentId = StudentId, AlphabeticGrade = 'A', NumericGrade = 89, CourseId = courseId};

            _sampleData = new SampleData();
            _mockGradeRepo = new Mock<IGradeRepository>();
            _mockGradeRepo.Setup(c => c.UpdateStudentGradeAsync(_mappedGrade)).ReturnsAsync(_mappedGrade);
            
            
            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<Grade>(_mappedGradeDto)).Returns(_mappedGrade);
            _mockMapper.Setup(m => m.Map<GradeDto>(_mappedGrade)).Returns(_mappedGradeDto);
            
        }

        [Fact]
        public async Task UpdateStudentGradeCommand_ReturnsUpdatedStudentEntity()
        {
            var command = new UpdateStudentGradeCommand(_mappedGradeDto.StudentId, _mappedGradeDto);
            var handler = new UpdateStudentGradeCommandHandler(_mockMapper.Object, _mockGradeRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<GradeDto>>(result);
            Assert.NotNull(result);
            Assert.Same(_mappedGradeDto, result.Data);
        }

        [Fact]
        public async Task UpdateStudentGradeCommand_ValidatesProperties()
        {
            GradeDto gradeDto = new GradeDto(){
                NumericGrade = 101,
                AlphabeticGrade = 'G'
            };

            var command = new UpdateStudentGradeCommand(gradeDto.StudentId, gradeDto);
            var validator = new UpdateStudentGradeCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.Equal(4, result.Errors.Count);
        }
    }
}