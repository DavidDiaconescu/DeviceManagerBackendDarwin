using DeviceManagement.API.Data;
using DeviceManagement.API.Models.Entities;
using DeviceManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.API.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _context;

    public DeviceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Device>> GetAllAsync()
    {
        return await _context.Devices
            .Include(d => d.AssignedUser)
            .ToListAsync();
    }

    public async Task<Device?> GetByIdAsync(int id)
    {
        return await _context.Devices
            .Include(d => d.AssignedUser)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Device> CreateAsync(Device device)
    {
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        return device;
    }

    public async Task<Device> UpdateAsync(Device device)
    {
        _context.Devices.Update(device);
        await _context.SaveChangesAsync();
        return device;
    }

    public async Task DeleteAsync(int id)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device is null)
            throw new KeyNotFoundException($"Device with id {id} was not found.");

        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string name, string manufacturer)
    {
        return await _context.Devices
            .AnyAsync(d => d.Name == name && d.Manufacturer == manufacturer);
    }

    public async Task<IEnumerable<Device>> GetAllWithUsersAsync()
    {
        return await _context.Devices
            .Include(d => d.AssignedUser)
            .ToListAsync();
    }
}
