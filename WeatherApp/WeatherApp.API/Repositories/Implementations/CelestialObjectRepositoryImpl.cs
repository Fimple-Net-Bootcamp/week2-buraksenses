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

    public async Task<CelestialObject?> GetByIdAsync(Guid id)
    {
        return await _dbContext.CelestialObjects.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CelestialObject> CreateAsync(CelestialObject celestialObject)
    {
        await _dbContext.CelestialObjects.AddAsync(celestialObject);
        await _dbContext.SaveChangesAsync();
        return celestialObject;
    }

    public async Task<CelestialObject?> UpdateAsync(Guid id, CelestialObject celestialObject)
    {
        var updatingObject = await GetByIdAsync(id);

        if (updatingObject == null)
            return null;

        updatingObject.Name = celestialObject.Name;
        updatingObject.TemperatureC = celestialObject.TemperatureC;
        updatingObject.Type = celestialObject.Type;

        await _dbContext.SaveChangesAsync();
        return updatingObject;
    }

    public async Task<CelestialObject?> DeleteAsync(Guid id)
    {
        var objectToDelete = await GetByIdAsync(id);

        if (objectToDelete == null)
            return null;

        _dbContext.Remove(objectToDelete);
        await _dbContext.SaveChangesAsync();
        
        return objectToDelete;
    }
}