using MediatR;
using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Requests;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class ActivityController : BaseAPIController
    {
        private readonly IAppLogger<ActivityController> _logger;

        public ActivityController(IAppLogger<ActivityController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivityList()
        {
            try
            {
                var command = new GetActivityListCommand();
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error fetching activity list {e} - {trace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpGet("{ActivityId}")]
        public async Task<IActionResult> GetPeopleInActivity([FromRoute] Guid ActivityId)
        {
            try
            {
                var command = new GetJoinedActivityListCommand(ActivityId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error get people in activity list {e} - {trace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonToActivity([FromBody] ActivityPersonRequest request)
        {
            try
            {
                var command = new AddToActivityListCommand(request);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error get people in activity list {e} - {trace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

       
    }
}