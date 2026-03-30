using DeviceManagement.API.Models.Enums;

namespace DeviceManagement.API.Models.DTOs;

public class DeviceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public DeviceType Type { get; set; }
    public string OperatingSystem { get; set; } = string.Empty;
    public string OSVersion { get; set; } = string.Empty;
    public string Processor { get; set; } = string.Empty;
    public int RAM { get; set; }
    public string? Description { get; set; }
    public int? AssignedUserId { get; set; }
    public string? AssignedUserName { get; set; }
}
