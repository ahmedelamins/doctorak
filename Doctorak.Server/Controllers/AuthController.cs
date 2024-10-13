﻿using Doctorak.Server.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Doctorak.Server.Controllers;

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
    public async Task<ActionResult> Register([FromBody] UserRegister request)
    {
        if (string.IsNullOrEmpty(request.Email))
        {
            return BadRequest("Email must be provided.");
        }

        var response = await _authService
            .Register(
                new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                },
                request.Password
            );

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login([FromBody] UserLogin request)
    {
        var response = await _authService.Login(request.Email, request.Password);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<ServiceResponse<string>>> RefreshToken([FromBody] RefreshToken request)
    {
        var response = await _authService.RefreshAccessToken(request.Token);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var response = await _authService.ForgotPassword(email);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);
    }

    [HttpPost("verify-password-reset")]
    public async Task<IActionResult> VerifyReset(string email, string code)
    {
        var response = await _authService.VerifyPasswordReset(email, code);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);
    }

    [HttpPost("change-password"), Authorize]
    public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePassword request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var response = await _authService.ChangePassword(int.Parse(userId), request.Password);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);

    }

    [HttpDelete("delete-user/{userId:int}"), Authorize]
    public async Task<ActionResult<bool>> DeleteUser(int userId)
    {
        var response = await _authService.DeleteUser(userId);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);
    }

    [HttpGet("fetch-user"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> FetchUsers()
    {
        var response = await _authService.FetchUsers();

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

}
