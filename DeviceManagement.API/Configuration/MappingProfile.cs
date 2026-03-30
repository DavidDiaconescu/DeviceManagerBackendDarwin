using AutoMapper;
using DeviceManagement.API.Models.DTOs;
using DeviceManagement.API.Models.Entities;
using DeviceManagement.API.Models.Requests;

namespace DeviceManagement.API.Configuration;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Device, DeviceDto>()
            .ForMember(dest => dest.AssignedUserName,
                opt => opt.MapFrom(src => src.AssignedUser != null ? src.AssignedUser.Name : null));

        CreateMap<CreateDeviceRequest, Device>();

        CreateMap<UpdateDeviceRequest, Device>();

        CreateMap<User, UserDto>();
    }
}
