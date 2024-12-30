using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using spendo_be.Models.DTO;
using spendo_be.Services;

namespace spendo_be.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId:int}")]
    public IActionResult Get(int userId)
    {
        var user = _userService.GetUserById(userId);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserDto userInfo)
    {
        var user = _userService.Create(userInfo);
        return CreatedAtAction(nameof(Get), new { userId = user.Id }, user);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] string email)
    {
        var user = _userService.GetUserByEmail(email);
        if (user == null)
        {
            return Unauthorized();
        }
        
        // Generate JWT
        var tokenHander = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            }),
            Expires = DateTime.UtcNow.AddDays(28),
            Issuer = "spendo_be",
            Audience = "spendo_api",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHander.CreateToken(tokenDescriptor);
        var tokenString = tokenHander.WriteToken(token);
        
        return Ok (new { Token = tokenString });
    }
}