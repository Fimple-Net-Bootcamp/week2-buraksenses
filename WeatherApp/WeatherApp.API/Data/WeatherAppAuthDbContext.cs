using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.API.Data;

public class WeatherAppAuthDbContext : IdentityDbContext
{
    public WeatherAppAuthDbContext(DbContextOptions<WeatherAppAuthDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        const string readerRoleId = "5EF04C1E-D052-4C35-B1DC-B933CC1E57FA";
        const string writerRoleId = "A7D0EE93-2B1F-4BC5-9C58-C70279CF68E3";

        var roles = new List<IdentityRole>
        {
            new()
            {
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper()
            },
            new()
            {
                Id = writerRoleId,
                ConcurrencyStamp = writerRoleId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper()
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}