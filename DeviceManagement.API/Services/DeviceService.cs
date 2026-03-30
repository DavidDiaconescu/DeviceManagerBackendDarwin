using AutoMapper;
using DeviceManagement.API.Models.DTOs;
using DeviceManagement.API.Models.Entities;
using DeviceManagement.API.Models.Requests;
using DeviceManagement.API.Repositories.Interfaces;
using DeviceManagement.API.Services.Interfaces;

namespace DeviceManagement.API.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DeviceService(IDeviceRepository deviceRepository, IUserRepository userRepository, IMapper mapper)
    {
        _deviceRepository = deviceRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DeviceDto>> GetAllAsync()
    {
        var devices = await _deviceRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DeviceDto>>(devices);
    }

    public async Task<DeviceDto?> GetByIdAsync(int id)
    {
        var device = await _deviceRepository.GetByIdAsync(id);
        return device is null ? null : _mapper.Map<DeviceDto>(device);
    }

    public async Task<DeviceDto> CreateAsync(CreateDeviceRequest request)
    {
        if (await _deviceRepository.ExistsAsync(request.Name, request.Manufacturer))
            throw new InvalidOperationException("Device already exists.");

        var device = _mapper.Map<Device>(request);
        var created = await _deviceRepository.CreateAsync(device);
        return _mapper.Map<DeviceDto>(created);
    }

    public async Task<DeviceDto> UpdateAsync(int id, UpdateDeviceRequest request)
    {
        var device = await _deviceRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Device with id {id} was not found.");

        _mapper.Map(request, device);
        var updated = await _deviceRepository.UpdateAsync(device);
        return _mapper.Map<DeviceDto>(updated);
    }

    public async Task DeleteAsync(int id)
    {
        await _deviceRepository.DeleteAsync(id);
    }

    public async Task<DeviceDto> AssignDeviceAsync(int deviceId, int userId)
    {
        var device = await _deviceRepository.GetByIdAsync(deviceId)
            ?? throw new KeyNotFoundException($"Device with id {deviceId} was not found.");

        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new KeyNotFoundException($"User with id {userId} was not found.");

        if (device.AssignedUserId is not null)
            throw new InvalidOperationException("Device already assigned.");

        device.AssignedUserId = userId;
        device.AssignedUser = user;
        var updated = await _deviceRepository.UpdateAsync(device);
        return _mapper.Map<DeviceDto>(updated);
    }

    public async Task<DeviceDto> UnassignDeviceAsync(int deviceId, int userId)
    {
        var device = await _deviceRepository.GetByIdAsync(deviceId)
            ?? throw new KeyNotFoundException($"Device with id {deviceId} was not found.");

        if (device.AssignedUserId != userId)
            throw new InvalidOperationException("Device not assigned to this user.");

        device.AssignedUserId = null;
        device.AssignedUser = null;
        var updated = await _deviceRepository.UpdateAsync(device);
        return _mapper.Map<DeviceDto>(updated);
    }

    public async Task<DeviceDto> UpdateDescriptionAsync(int id, string description)
    {
        var device = await _deviceRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Device with id {id} was not found.");

        device.Description = description;
        var updated = await _deviceRepository.UpdateAsync(device);
        return _mapper.Map<DeviceDto>(updated);
    }
}
