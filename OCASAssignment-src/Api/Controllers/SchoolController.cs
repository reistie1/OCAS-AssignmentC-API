
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

        public SchoolController(IAppLogger<SchoolController> logger)
        {
            _logger = logger;
        }


        [HttpPatch]
        public async Task<IActionResult> UpdateSchoolInformation([FromBody] SchoolInfoRequest request)
        {
            try
            {
                var command = new UpdateSchoolInformationCommand(request);
                var result = await this._mediator.Send(command);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error updating school information {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSchoolInformation([FromQuery] Guid SchoolId)
        {
            try
            {
                var command = new GetSchoolInformationCommand(SchoolId);
                var result = await this._mediator.Send(command);

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