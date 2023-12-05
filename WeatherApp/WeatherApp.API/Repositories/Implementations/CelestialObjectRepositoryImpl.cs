using Microsoft.EntityFrameworkCore;
using WeatherApp.API.Data;
using WeatherApp.API.Models.Domains;

namespace WeatherApp.API.Repositories.Implementations;

public class CelestialObjectRepositoryImpl : ICelestialObjectRepository
{
    private readonly WeatherAppDbContext _dbContext;

    public CelestialObjectRepositoryImpl(WeatherAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<CelestialObject>> GetAllAsync()
    {
        return await _dbContext.CelestialObjects.ToListAsync();
    }

    public Task<CelestialObject?> GetByIdAsync(Guid id)
    {
        return null;
    }

    public Task<CelestialObject> CreateAsync(CelestialObject celestialObject)
    {
        return null;
    }

    public Task<CelestialObject?> UpdateAsync(Guid id, CelestialObject celestialObject)
    {
        return null;
    }

    public Task<CelestialObject?> DeleteAsync(Guid id)
    {
        return null;
    }
}