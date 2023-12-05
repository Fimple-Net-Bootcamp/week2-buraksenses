namespace WeatherApp.API.Models.DTOs;

public class CelestialObjectDto
{
    public string Name { get; set; }
    public float TemperatureC { get; set; }
    
    public float TemperatureF => 32 + TemperatureC / 0.5556f;

    public string ObjectType { get; set; }
}