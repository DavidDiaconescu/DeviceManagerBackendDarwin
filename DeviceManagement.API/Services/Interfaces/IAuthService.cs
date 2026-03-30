using DeviceManagement.API.Models.DTOs;
using DeviceManagement.API.Models.Requests;

namespace DeviceManagement.API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequest request);
    Task<AuthResponseDto> LoginAsync(LoginRequest request);
}
