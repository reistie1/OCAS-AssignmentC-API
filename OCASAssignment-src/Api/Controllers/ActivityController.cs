using MediatR;
using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Parameters;
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

        /// <summary>
        /// Gets the list of activities that a person can sign up for
        /// </summary>
        /// <returns>List of actvities</returns>
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

        /// <summary>
        /// Gets list of people currently signed up for a given activity
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <returns>List of people signed up for the requested activity</returns>
        [HttpGet("{ActivityId}")]
        public async Task<IActionResult> GetPeopleInActivity([FromRoute] Guid ActivityId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var requestParams = new RequestParameters(pageNumber, pageSize, null);
                var command = new GetJoinedActivityListCommand(ActivityId, requestParams);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error get people in activity list {e} - {trace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Register a person for a given activity
        /// </summary>
        /// <param name="request"></param>
        /// <returns><see cref="System.Boolean"> representing the result of the register operation</returns>
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