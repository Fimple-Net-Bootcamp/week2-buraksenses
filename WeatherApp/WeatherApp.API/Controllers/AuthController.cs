using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Models.DTOs;
using WeatherApp.API.Repositories;

namespace WeatherApp.API.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRepository _tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }
    
    //POST: /api/v1/auth/register
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        var identityUser = new IdentityUser
        {
            UserName = requestDto.Username,
            Email = requestDto.Username
        };

        var identityResult = await _userManager.CreateAsync(identityUser, requestDto.Password);

        if (identityResult.Succeeded)
        {
            if (requestDto.Roles.Any())
            { 
                identityResult = await _userManager.AddToRolesAsync(identityUser, requestDto.Roles);
                if (identityResult.Succeeded)
                {
                    return Ok("User was registered! Please login.");
                }
            }
            
        }

        return BadRequest("Something went wrong!");
    }
    
    //POST: /api/v1/auth/login
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

        if (user != null)
        {
            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (checkPasswordResult)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Any())
                {
                    var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());
                    var response = new LoginResponseDto
                    {
                        JwtToken = jwtToken
                    };
                    return Ok(response);
                }
            }
        }

        return BadRequest("Username or password incorrect");
    }
}