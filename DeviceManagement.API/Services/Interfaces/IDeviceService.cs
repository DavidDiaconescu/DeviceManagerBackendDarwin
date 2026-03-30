using DeviceManagement.API.Models.DTOs;
using DeviceManagement.API.Models.Requests;

namespace DeviceManagement.API.Services.Interfaces;

public interface IDeviceService
{
    Task<IEnumerable<DeviceDto>> GetAllAsync();
    Task<DeviceDto?> GetByIdAsync(int id);
    Task<DeviceDto> CreateAsync(CreateDeviceRequest request);
    Task<DeviceDto> UpdateAsync(int id, UpdateDeviceRequest request);
    Task DeleteAsync(int id);
    Task<DeviceDto> AssignDeviceAsync(int deviceId, int userId);
    Task<DeviceDto> UnassignDeviceAsync(int deviceId, int userId);
    Task<DeviceDto> UpdateDescriptionAsync(int id, string description);
}
