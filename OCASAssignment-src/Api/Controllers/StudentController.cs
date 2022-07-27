
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class StudentController : BaseAPIController
    {
        private readonly IAppLogger<StudentController> _logger;

        public StudentController(IAppLogger<StudentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto Student)
        {
            try
            {
                var command = new AddSchoolStudentCommand(Student);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error adding school student {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpGet("{SchoolId}")]
        public async Task<IActionResult> GetStudents([FromRoute] Guid SchoolId)
        {
            try
            {
                var command = new GetSchoolStudentsCommand(SchoolId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error adding school student {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpPost("course/{StudentId}")]
        public async Task<IActionResult> AddCourse([FromRoute] Guid StudentId, [FromQuery] Guid courseId)
        {
            try
            {
                var command = new AddStudentCourseCommand(StudentId, courseId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error adding student to course {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentDto Student)
        {
            try
            {
                var command = new UpdateSchoolStudentCommand(Student);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error updating student to course {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpDelete("course/{CourseId}")]
        public async Task<IActionResult> RemoveCourse([FromRoute] Guid CourseId)
        {
            try
            {
                var command = new DeleteCourseCommand(CourseId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error removing school course {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpDelete("{StudentId}")]
        public async Task<IActionResult> RemoveStudent([FromRoute] Guid StudentId)
        {
            try
            {
                var command = new DeleteSchoolStudentCourseCommand(StudentId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error removing school course {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpGet("enrolled/{StudentId}")]
        public async Task<IActionResult> GetEnrolledCourses([FromRoute] Guid StudentId)
        {
            try
            {
                var command = new GetStudentCoursesCommand(StudentId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error fetching enrolled courses {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }
    }
}