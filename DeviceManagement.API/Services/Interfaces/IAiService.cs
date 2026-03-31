using DeviceManagement.API.Models.DTOs;

namespace DeviceManagement.API.Services.Interfaces;

public interface IAiService
{
    Task<string> GenerateDeviceDescriptionAsync(DeviceDto device);
    Task<string> GenerateProcurementSuggestionAsync(decimal budgetPerEmployee, int employeeCount, string currency);
}
