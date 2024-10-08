﻿//using Microsoft.AspNetCore.Mvc;

//namespace Doctorak.Server.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class AuthController : ControllerBase
//{
//    private readonly IAuthService _authService;

//    public AuthController(IAuthService authService)
//    {
//        _authService = authService;
//    }

//    [HttpPost("register")]
//    public async Task<ActionResult<ServiceResponse<int>>> Register([FromBody] UserRegister request)
//    {
//        if (string.IsNullOrEmpty(request.Email))
//        {
//            return BadRequest("an Email must be provided.");
//        }

//        var response = await _authService
//            .Register(
//                new User
//                {
//                    Email = request.Email,
//                    FirstName = request.FirstName,
//                    LastName = request.LastName,
//                },
//                request.Password
//            );

//        if (!response.Success)
//        {
//            return BadRequest(response.Message);
//        }


//        return Ok(response);
//    }

//    [HttpGet("confirm-email")]
//    public async Task<ActionResult<ServiceResponse<bool>>> ConfirmEmail(string token, string email)
//    {
//        var response = await _authService.ConfirmEmail(token, email);

//        if (!response.Success)
//        {
//            return BadRequest(response.Message);
//        }

//        return Ok(response);
//    }
//}
