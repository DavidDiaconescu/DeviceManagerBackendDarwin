using DeviceManagement.API.Models.Enums;

namespace DeviceManagement.API.Models.Requests;

public class CreateDeviceRequest
{
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public DeviceType Type { get; set; }
    public string OperatingSystem { get; set; } = string.Empty;
    public string OSVersion { get; set; } = string.Empty;
    public string Processor { get; set; } = string.Empty;
    public int RAM { get; set; }
}
