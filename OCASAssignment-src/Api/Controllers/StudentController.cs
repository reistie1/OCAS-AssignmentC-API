
using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Parameters;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class StudentController : BaseAPIController
    {
        private readonly IAppLogger<StudentController> _logger;

        public StudentController(IAppLogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CourseDto Course)
        {
            try
            {
                var command = new AddCourseCommand(Course);
                var result = await this._mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error adding school course {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCourse([FromQuery] Guid CourseId)
        {
            try
            {
                var command = new DeleteCourseCommand(CourseId);
                var result = await this._mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error removing school course {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpPost("/enrolled/{SchoolId}")]
        public async Task<IActionResult> GetEnrolledCourses([FromRoute] Guid SchoolId, [FromBody] RequestParameters requestParameters)
        {
            try
            {
                var command = new GetCourseListCommand(SchoolId, requestParameters);
                var result = await this._mediator.Send(command);

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