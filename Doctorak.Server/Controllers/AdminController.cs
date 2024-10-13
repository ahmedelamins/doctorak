using Microsoft.AspNetCore.Mvc;

namespace Doctorak.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterAdmin request)
    {
        var response = await _adminService
            .AdminRegister(
                new Admin
                {
                    Username = request.Username
                },
                request.Password
            );

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);
    }
}
