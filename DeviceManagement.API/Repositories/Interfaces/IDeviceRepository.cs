using DeviceManagement.API.Models.Entities;

namespace DeviceManagement.API.Repositories.Interfaces;

public interface IDeviceRepository
{
    Task<IEnumerable<Device>> GetAllAsync();
    Task<Device?> GetByIdAsync(int id);
    Task<Device> CreateAsync(Device device);
    Task<Device> UpdateAsync(Device device);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(string name, string manufacturer);
    Task<IEnumerable<Device>> GetAllWithUsersAsync();
}
