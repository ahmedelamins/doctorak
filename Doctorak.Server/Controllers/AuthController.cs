using Microsoft.AspNetCore.Mvc;

namespace Doctorak.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register([FromBody] UserRegister request)
        {
            if (string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Number))
            {
                return BadRequest("Either Email or Phone number must be provided.");
            }

            //format the number correctly
            string formattedNumber = string.IsNullOrEmpty(request.Number) ? null : $"+249{request.Number}";

            var response = await _authService
                .Register(
                    new User
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        Number = formattedNumber,
                    },
                    request.Password
                );

            if (!response.Success)
            {
                return BadRequest(response);
            }


            return Ok(response);
        }
    }
}
