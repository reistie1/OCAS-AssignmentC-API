
using Microsoft.AspNetCore.Mvc;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class TeacherController : BaseAPIController
    {
        public TeacherController()
        {}

        [HttpPost("{SchoolId}")]
        public async Task<IActionResult> AddTeacherToCourse([FromQuery] Guid SchoolId)
        {
            return Ok();
        }

        [HttpGet("{StudentId}")]
        public async Task<IActionResult> GetTeacherCourses([FromQuery] Guid StudentId)
        {
            return Ok();
        }

        [HttpPatch("{StudentId}")]
        public async Task<IActionResult> ChangeCourseTeacher([FromQuery] Guid StudentId)
        {
            return Ok();
        }

        [HttpDelete("{StudentId}")]
        public async Task<IActionResult> RemoveTeacherFromCourse([FromQuery] Guid SchoolId)
        {
            return Ok();
        }
    }
}