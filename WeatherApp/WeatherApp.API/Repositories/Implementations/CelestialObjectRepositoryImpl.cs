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
    
    public async Task<List<CelestialObject>> GetAllAsync(string? filterOn = null,string? filterQuery = null, string? sortBy = null,
        bool? isAscending = null,int pageNumber = 1,int pageSize = 1000)
    {
        var celestialObjects = _dbContext.CelestialObjects.AsQueryable();
        
        //Filtering
        if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
        {
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                celestialObjects = celestialObjects.Where(x => x.Name.Contains(filterQuery));
            else if (filterOn.Equals("Temperature", StringComparison.OrdinalIgnoreCase))
                celestialObjects = celestialObjects.Where(x => Math.Abs(x.TemperatureC - float.Parse(filterQuery)) < .1f);
            else if (filterOn.Equals("Type", StringComparison.OrdinalIgnoreCase))
                celestialObjects = celestialObjects.Where(x => x.ObjectType == filterQuery);
        }
        
        //Sorting
        if (!string.IsNullOrWhiteSpace(sortBy) && isAscending != null)
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                celestialObjects = isAscending.Value
                    ? celestialObjects.OrderBy(x => x.Name)
                    : celestialObjects.OrderByDescending(x => x.Name);
            else if(sortBy.Equals("Temperature",StringComparison.OrdinalIgnoreCase))
                celestialObjects = isAscending.Value
                    ? celestialObjects.OrderBy(x => x.TemperatureC)
                    : celestialObjects.OrderByDescending(x => x.TemperatureC);
            else if(sortBy.Equals("Type",StringComparison.OrdinalIgnoreCase))
                celestialObjects = isAscending.Value
                    ? celestialObjects.OrderBy(x => x.ObjectType)
                    : celestialObjects.OrderByDescending(x => x.ObjectType);
        }
        
        return await celestialObjects.ToListAsync();
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
        updatingObject.ObjectType = celestialObject.ObjectType;

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