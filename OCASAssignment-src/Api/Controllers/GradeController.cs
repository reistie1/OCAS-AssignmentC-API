
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class GradeController : BaseAPIController
    {
        private readonly IAppLogger<GradeController> _logger;

        public GradeController(IAppLogger<GradeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("{SchoolId}")]
        public async Task<IActionResult> AddStudentGrade([FromQuery] Guid SchoolId, [FromBody] GradeDto Grade)
        {
            try
            {
                var command = new AddStudentGradeCommand(SchoolId, Grade);
                var result = await this._mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error adding requested student grade {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpGet("{CourseId}")]
        public async Task<IActionResult> GetStudentGrade([FromRoute] Guid CourseId, [FromQuery] Guid StudentId)
        {
            try
            {
                var command = new GetStudentCourseGradeCommand(StudentId, CourseId);
                var result = await this._mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error fetching student grade for requested course {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpPatch("{StudentId}")]
        public async Task<IActionResult> UpdateStudentGrade([FromQuery] Guid StudentId, [FromBody] GradeDto grade)
        {
            try
            {
                var command = new UpdateStudentGradeCommand(StudentId, grade);
                var result = await this._mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error updating student course grade {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpDelete("{CourseId}")]
        public async Task<IActionResult> RemoveStudentGrade([FromRoute] Guid CourseId, [FromQuery] Guid StudentId)
        {
            try
            {
                var command = new DeleteStudentGradeCommand(StudentId, CourseId);
                var result = await this._mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error removing student grade {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }
    }
}