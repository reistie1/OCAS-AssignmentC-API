
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class SchoolController : BaseAPIController
    {
        private readonly IAppLogger<SchoolController> _logger;

        public SchoolController(IAppLogger<SchoolController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpPatch]
        public async Task<IActionResult> UpdateSchoolInformation([FromBody] SchoolInfoRequest request)
        {
            try
            {
                var command = new UpdateSchoolInformationCommand(request);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error updating school information {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpGet("{SchoolId}")]
        public async Task<IActionResult> GetSchoolInformation([FromRoute] Guid SchoolId)
        {
            try
            {
                var command = new GetSchoolInformationCommand(SchoolId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error fetching school information {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }
    }
}