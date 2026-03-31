using DeviceManagement.API.Models.DTOs;
using DeviceManagement.API.Models.Requests;
using DeviceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ProcurementController : ControllerBase
{
    private readonly IAiService _aiService;

    public ProcurementController(IAiService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("suggest")]
    public async Task<IActionResult> GetSuggestion([FromBody] ProcurementRequest request)
    {
        if (request.TotalBudget <= 0 || request.EmployeeCount <= 0)
            return BadRequest("Budget and employee count must be greater than 0.");

        var budgetPerEmployee = request.TotalBudget / request.EmployeeCount;

        var suggestion = await _aiService.GenerateProcurementSuggestionAsync(
            budgetPerEmployee, request.EmployeeCount, request.Currency ?? "RON");

        return Ok(new ProcurementSuggestionDto
        {
            TotalBudget = request.TotalBudget,
            EmployeeCount = request.EmployeeCount,
            BudgetPerEmployee = budgetPerEmployee,
            Currency = request.Currency ?? "RON",
            Suggestion = suggestion
        });
    }
}
