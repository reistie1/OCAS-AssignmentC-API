
using Microsoft.AspNetCore.Mvc;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class GradeController : BaseAPIController
    {
        public GradeController()
        {
        }

        [HttpPost("{SchoolId}")]
        public async Task<IActionResult> AddStudentGrade([FromQuery] Guid SchoolId)
        {
            return Ok();
        }

        [HttpGet("{StudentId}")]
        public async Task<IActionResult> GetStudentGrade([FromQuery] Guid StudentId)
        {
            return Ok();
        }

        [HttpPatch("{StudentId}")]
        public async Task<IActionResult> UpdateStudentGrade([FromQuery] Guid StudentId)
        {
            return Ok();
        }

        [HttpDelete("{StudentId}")]
        public async Task<IActionResult> RemoveStudentGrade([FromQuery] Guid SchoolId)
        {
            return Ok();
        }
    }
}