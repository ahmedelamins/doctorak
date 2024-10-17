﻿using Doctorak.Server.Services.DoctorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Doctorak.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{

    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet("fetch-slots/{doctorId:int}")]
    public async Task<ActionResult> FetchSlots(int doctorId)
    {
        var response = await _doctorService.FetchAvailabilitySlots(doctorId);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("/add-slot"), Authorize(Roles = "Doctor")]
    public async Task<ActionResult> CreateSlot([FromBody] CreateSlot request)
    {
        var doctorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        // Map the DTO to AvailabilitySlot model
        var slot = new AvailabilitySlot
        {
            DoctorId = doctorId,
            Day = request.Day,
            Starts = request.Starts,
            Ends = request.Ends
        };

        var response = await _doctorService.CreateAvailabilitySlot(doctorId, slot);

        return response.Success ? Ok(response) : BadRequest(response);
    }

}
