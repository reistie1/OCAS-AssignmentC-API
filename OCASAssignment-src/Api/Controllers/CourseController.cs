using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.Parameters;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class CourseController : BaseAPIController
    {
        public CourseController()
        {}

        [HttpPost("{SchoolId}")]
        public async Task<IActionResult> AddSchoolCourse([FromRoute] Guid SchoolId)
        {
            return Ok();
        }


        [HttpPatch("{CourseId}")]
        public async Task<IActionResult> EditSchoolCourse([FromRoute] Guid CourseId)
        {
            return Ok();
        }

        [HttpDelete("{CourseId}")]
        public async Task<IActionResult> DeleteSchoolCourse([FromRoute] Guid CourseId)
        {
            return Ok();
        }

        [HttpGet("{SchoolId}")]
        public async Task<IActionResult> GetSchoolCourses([FromRoute] Guid SchoolId, [FromQuery] RequestParameters requestParameters)
        {
            return Ok();
        }
    }
}