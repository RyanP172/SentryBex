using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SentryBex.Dtos;
using SentryBex.Models;
using SentryBex.Services.Authentication;

namespace SentryBex.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {

            _authenticationRepository = authenticationRepository;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> UserRegister([FromBody] AspNetUserRegisterDto registerBody)
        {
            if (await _authenticationRepository.CheckEmailExist(registerBody))
            {
                return BadRequest(new { status = 400, message = $"User email {registerBody.Email} already existed" });
            }
            var response = await _authenticationRepository.Register(registerBody);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] LogInDto loginBody)
        {

            var response = await _authenticationRepository.Login(loginBody);
            if (response)
            {
                var result = await _authenticationRepository.AssignJwtToken(loginBody);
                return Ok(new {status = 200, token=result, description="Login successful!" });
            }
            else
            {
                return BadRequest(new
                {
                    status = 400,
                    description = "Login failed, please check your credentials."
                });
            }
        }
    }
}
