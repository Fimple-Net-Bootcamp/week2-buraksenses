using AutoMapper;
using WeatherApp.API.Models.Domains;
using WeatherApp.API.Models.DTOs;

namespace WeatherApp.API.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CelestialObject, CelestialObjectDto>().ReverseMap();
    }
}