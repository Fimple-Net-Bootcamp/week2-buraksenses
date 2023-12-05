using Microsoft.AspNetCore.Identity;

namespace WeatherApp.API.Repositories;

public interface ITokenRepository
{
    string CreateJwtToken(IdentityUser user, List<string> roles);
}