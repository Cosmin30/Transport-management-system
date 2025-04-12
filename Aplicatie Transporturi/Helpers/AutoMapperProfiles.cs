using Aplicatie_Transporturi.Entities;
using AutoMapper;
using Aplicatie_Transporturi.DTOs;
namespace Aplicatie_Transporturi.Helpers { 
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
        CreateMap<Driver, DriverDto>().ReverseMap();
        CreateMap<Delivery, DeliveryDto>().ReverseMap();
    }
}
}