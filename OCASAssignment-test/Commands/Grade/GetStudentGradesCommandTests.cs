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
    public class GetStudentGradesCommandTests
    {
        private readonly Mock<IGradeRepository> _mockGradeRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly List<GradeDto> _mappedGradeDto;
        private readonly List<Grade> _mappedGrade;

        public GetStudentGradesCommandTests()
        {
            Guid courseId = Guid.NewGuid();
            Guid StudentId = Guid.NewGuid();

            _mappedGradeDto = new List<GradeDto>()
            {
                new GradeDto(){Id = Guid.NewGuid(), StudentId = StudentId, AlphabeticGrade = 'A', NumericGrade = 89, CourseId = courseId}
            };

            _mappedGrade = new List<Grade>()
            {
                new Grade(){Id = Guid.NewGuid(), StudentId = StudentId, AlphabeticGrade = 'A', NumericGrade = 89, CourseId = courseId}
            };

            _mockGradeRepo = new Mock<IGradeRepository>();
            _mockGradeRepo.Setup(c => c.GetStudentGradesAsync(StudentId)).ReturnsAsync(_mappedGrade);
            
            
            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<IReadOnlyList<GradeDto>>(_mappedGrade)).Returns(_mappedGradeDto);
            _mockMapper.Setup(m => m.Map<IReadOnlyList<Grade>>(_mappedGradeDto)).Returns(_mappedGrade);            
        }

        [Fact]
        public async Task GetStudentCourseGradeCommand_ReturnsStudentGradeEntity()
        {
            var command = new GetStudentGradesCommand(_mappedGradeDto.First().StudentId);
            var handler = new GetStudentGradesCommandHandler(_mockMapper.Object, _mockGradeRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<IReadOnlyList<GradeDto>>>(result);
            Assert.NotNull(result);
            Assert.Equal(_mappedGradeDto, result.Data);
        }
    }
}