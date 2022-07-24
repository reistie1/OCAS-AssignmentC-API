
using Microsoft.AspNetCore.Mvc;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class SchoolController : BaseAPIController
    {
        public SchoolController()
        {
            
        }


        [HttpPatch]
        public async Task<IActionResult> UpdateSchoolInformation()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSchoolInformation()
        {
            return Ok();
        }
    }
}