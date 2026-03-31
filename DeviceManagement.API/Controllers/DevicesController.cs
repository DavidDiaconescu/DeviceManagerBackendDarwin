using System.Security.Claims;
using DeviceManagement.API.Models.Requests;
using DeviceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DevicesController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [AllowAnonymous]
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        var results = await _deviceService.SearchAsync(query ?? string.Empty);
        return Ok(results);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var devices = await _deviceService.GetAllAsync();
        return Ok(devices);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var device = await _deviceService.GetByIdAsync(id);
        return device is null ? NotFound() : Ok(device);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDeviceRequest request)
    {
        try
        {
            var created = await _deviceService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDeviceRequest request)
    {
        try
        {
            var updated = await _deviceService.UpdateAsync(id, request);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _deviceService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{id:int}/assign")]
    public async Task<IActionResult> Assign(int id, [FromBody] AssignByAdminRequest? request = null)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return Unauthorized();
        var currentUserId = int.Parse(userIdClaim);
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        var targetUserId = currentUserId;
        if (role == "Admin" && request?.UserId is not null)
            targetUserId = request.UserId.Value;

        try
        {
            var result = await _deviceService.AssignDeviceAsync(id, targetUserId);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/description")]
    public async Task<IActionResult> UpdateDescription(int id, [FromBody] string description)
    {
        try
        {
            var updated = await _deviceService.UpdateDescriptionAsync(id, description);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:int}/force-unassign")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ForceUnassign(int id)
    {
        try
        {
            var result = await _deviceService.ForceUnassignAsync(id);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}/unassign")]
    public async Task<IActionResult> Unassign(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return Unauthorized();
        var currentUserId = int.Parse(userIdClaim);
        var isAdmin = User.FindFirst(ClaimTypes.Role)?.Value == "Admin";

        try
        {
            var result = await _deviceService.UnassignDeviceAsync(id, currentUserId, isAdmin);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
