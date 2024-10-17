using Doctorak.Server.Services.DoctorService;
using Microsoft.AspNetCore.Mvc;

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

    //public async Task<ActionResult>
    //public async Task<ActionResult>
    //public async Task<ActionResult>
}
