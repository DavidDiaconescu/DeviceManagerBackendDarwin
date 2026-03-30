using DeviceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.API.Controllers;

public class GenerateDescriptionRequest
{
    public int DeviceId { get; set; }
}

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AiController : ControllerBase
{
    private readonly IDeviceService _deviceService;
    private readonly IAiService _aiService;

    public AiController(IDeviceService deviceService, IAiService aiService)
    {
        _deviceService = deviceService;
        _aiService = aiService;
    }

    [HttpPost("generate-description")]
    public async Task<IActionResult> GenerateDescription([FromBody] GenerateDescriptionRequest request)
    {
        var device = await _deviceService.GetByIdAsync(request.DeviceId);
        if (device is null)
            return NotFound($"Device with id {request.DeviceId} was not found.");

        try
        {
            var description = await _aiService.GenerateDeviceDescriptionAsync(device);
            return Ok(description);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
