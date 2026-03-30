using System.Text.RegularExpressions;
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

    public async Task<IEnumerable<SearchResultDto>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Enumerable.Empty<SearchResultDto>();

        var normalized = Regex.Replace(query.Trim().ToLower(), @"[^\w\s]", " ");
        var tokens = normalized.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length == 0)
            return Enumerable.Empty<SearchResultDto>();

        var devices = await _deviceRepository.GetAllWithUsersAsync();

        var results = new List<SearchResultDto>();

        foreach (var device in devices)
        {
            var score = 0;
            var name = device.Name.ToLower();
            var manufacturer = device.Manufacturer.ToLower();
            var processor = device.Processor.ToLower();
            var ram = device.RAM.ToString();

            foreach (var token in tokens)
            {
                if (name.Contains(token))         score += 10;
                if (manufacturer.Contains(token)) score += 6;
                if (processor.Contains(token))    score += 4;
                if (ram.Contains(token))          score += 2;
            }

            if (score > 0)
            {
                var dto = _mapper.Map<SearchResultDto>(device);
                dto.Score = score;
                results.Add(dto);
            }
        }

        return results.OrderByDescending(r => r.Score);
    }
}
