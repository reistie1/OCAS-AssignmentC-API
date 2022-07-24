
using Microsoft.AspNetCore.Mvc;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class StudentController : BaseAPIController
    {
        public StudentController()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCourse()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetEnrolledCourses()
        {
            return Ok();
        }
    }
}