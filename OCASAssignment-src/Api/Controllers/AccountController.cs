
using Microsoft.AspNetCore.Mvc;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class AccountController : BaseAPIController
    {
        public AccountController()
        {
            
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }


    }
}