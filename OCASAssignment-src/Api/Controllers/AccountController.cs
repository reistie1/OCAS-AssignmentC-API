using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.DTO.Requests;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class AccountController : BaseAPIController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            try
            {
                var result = await _accountService.Register(request);

                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _accountService.Login(request);

                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }


    }
}