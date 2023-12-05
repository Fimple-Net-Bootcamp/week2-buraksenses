﻿using Microsoft.EntityFrameworkCore;
using WeatherApp.API.Models.Domains;

namespace WeatherApp.API.Data;

public class WeatherAppDbContext : DbContext
{
    public WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options) : base(options)
    {
        
    }

    public DbSet<CelestialObject> CelestialObjects { get; set; }
}