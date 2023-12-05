using WeatherApp.API.Utils;

namespace WeatherApp.API.Models.DTOs;

public class CelestialObjectDto
{
    public string Name { get; set; }
    public float TemperatureC { get; set; }
    
    public float TemperatureF => 32 + TemperatureC / 0.5556f;

    public CelestialObjectType Type { get; set; }
}