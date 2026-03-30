using System.ComponentModel.DataAnnotations;
using DeviceManagement.API.Models.Enums;

namespace DeviceManagement.API.Models.Entities;

public class Device
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Manufacturer { get; set; } = string.Empty;

    public DeviceType Type { get; set; }

    [Required]
    public string OperatingSystem { get; set; } = string.Empty;

    [Required]
    public string OSVersion { get; set; } = string.Empty;

    [Required]
    public string Processor { get; set; } = string.Empty;

    public int RAM { get; set; }

    public string? Description { get; set; }

    public int? AssignedUserId { get; set; }

    public User? AssignedUser { get; set; }
}
