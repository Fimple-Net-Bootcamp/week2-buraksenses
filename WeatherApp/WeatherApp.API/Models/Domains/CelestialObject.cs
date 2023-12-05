namespace WeatherApp.API.Models.Domains;

public class CelestialObject
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    public float TemperatureC { get; set; }
    
    public float TemperatureF => 32 + TemperatureC / 0.5556f;

    public string ObjectType { get; set; }
}