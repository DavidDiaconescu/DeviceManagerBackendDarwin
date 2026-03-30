using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.API.Models.Entities;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;
}
