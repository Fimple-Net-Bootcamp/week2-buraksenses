using WeatherApp.API.Models.Domains;

namespace WeatherApp.API.Repositories;

public interface ICelestialObjectRepository
{
    Task<List<CelestialObject>> GetAllAsync();

    Task<CelestialObject?> GetByIdAsync(Guid id);

    Task<CelestialObject> CreateAsync(CelestialObject celestialObject);

    Task<CelestialObject?> UpdateAsync(Guid id, CelestialObject celestialObject);

    Task<CelestialObject?> DeleteAsync(Guid id);
}