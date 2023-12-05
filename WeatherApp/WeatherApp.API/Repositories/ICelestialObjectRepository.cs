using WeatherApp.API.Models.Domains;

namespace WeatherApp.API.Repositories;

public interface ICelestialObjectRepository
{
    Task<List<CelestialObject>> GetAllAsync(string? filterOn = null,string? filterQuery = null, string? sortBy = null,bool? isAscending = null,int pageNumber = 1,int pageSize = 1000);

    Task<CelestialObject?> GetByIdAsync(Guid id);

    Task<CelestialObject> CreateAsync(CelestialObject celestialObject);

    Task<CelestialObject?> UpdateAsync(Guid id, CelestialObject celestialObject);

    Task<CelestialObject?> DeleteAsync(Guid id);
}