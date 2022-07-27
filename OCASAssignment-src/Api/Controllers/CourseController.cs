using MediatR;
using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Parameters;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class CourseController : BaseAPIController
    {
        private readonly IAppLogger<CourseController> _logger;

        public CourseController(IAppLogger<CourseController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddSchoolCourse([FromBody] CourseDto request)
        {
            try
            {
                var command = new AddCourseCommand(request);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error adding requested course {error} - {stackTrace} - {innerException}", e.Message, e.StackTrace, e.InnerException);
                return BadRequest(e);
            }
        }


        [HttpPatch]
        public async Task<IActionResult> EditSchoolCourse([FromBody] CourseDto request)
        {
            try
            {
                var command = new EditCourseCommand(request);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error editing requested course {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpDelete("{CourseId}")]
        public async Task<IActionResult> DeleteSchoolCourse([FromRoute] Guid CourseId)
        {
            try
            {
                var command = new DeleteCourseCommand(CourseId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error deleting request course {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpPost("list/{SchoolId}")]
        public async Task<IActionResult> GetSchoolCourses([FromRoute] Guid SchoolId, [FromBody] RequestParameters requestParameters)
        {
            try
            {
                var command = new GetCourseListCommand(SchoolId, requestParameters);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error fetching course list {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpGet("{CourseId}")]
        public async Task<IActionResult> GetSchoolCourse([FromRoute] Guid CourseId)
        {
            try
            {
                var command = new GetCourseCommand(CourseId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error fetching requested course {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }
    }
}