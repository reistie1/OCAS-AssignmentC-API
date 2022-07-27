
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class TeacherController : BaseAPIController
    {
        private readonly IAppLogger<StudentController> _logger;

        public TeacherController(IAppLogger<StudentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("{CourseId}")]
        public async Task<IActionResult> AddTeacherToCourse([FromRoute] Guid CourseId, [FromQuery] Guid TeacherId)
        {
            try
            {
                var command = new AddCourseTeacherCommand(CourseId, TeacherId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error adding course teacher {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpGet("{TeacherId}")]
        public async Task<IActionResult> GetTeacherCourses([FromRoute] Guid TeacherId)
        {
            try
            {
                var command = new GetTeacherCoursesCommand(TeacherId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error fetching teacher courses {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpGet("list/{SchoolId}")]
        public async Task<IActionResult> GetSchoolTeachers([FromRoute] Guid SchoolId)
        {
            try
            {
                var command = new GetSchoolTeachersCommand(SchoolId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error fetching teacher courses {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpPatch("{StudentId}")]
        public async Task<IActionResult> ChangeCourseTeacher([FromBody] ChangeTeacherRequest request)
        {
            try
            {
                var command = new ChangeCourseTeacherCommand(request);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error changing course teacher {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        // [HttpDelete("{CourseId}")]
        // public async Task<IActionResult> RemoveTeacherFromCourse([FromRoute] Guid CourseId, [FromQuery] Guid TeacherId)
        // {
        //     try
        //     {
        //         var command = new RemoveCourseTeacherCommand(CourseId, TeacherId);
        //         var result = await _mediator.Send(command);

        //         return Ok(result);
        //     }
        //     catch(Exception e)
        //     {
        //         _logger.LogWarning("Error removing course teacher {error} - {stackTrace}", e.Message, e.StackTrace);
        //         return BadRequest(e);
        //     }
        // }

        [HttpPost]
        public async Task<IActionResult> AddSchoolTeacher([FromBody] TeacherDto teacher)
        {
            try
            {
                var command = new AddSchoolTeacherCommand(teacher);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error adding school teacher {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateSchoolTeacher([FromBody] TeacherDto teacher)
        {
            try
            {
                var command = new UpdateSchoolTeacherCommand(teacher);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error adding school teacher {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpDelete("{TeacherId}")]
        public async Task<IActionResult> DeleteSchoolTeacher([FromRoute] Guid TeacherId)
        {
            try
            {
                var command = new DeleteSchoolTeacherCommand(TeacherId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error deleting school teacher {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }
    }
}