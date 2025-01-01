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

    [HttpGet("{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        
        return Ok(user);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserDto userInfo)
    {
        var userExist = _userService.DoesUserExist(userInfo.Email);

        if (userExist)
        {
            return Conflict();
        }
        
        var user = _userService.Create(userInfo);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] string email)
    {
        var user = _userService.GetUserByEmail(email);
        if (user == null)
        {
            return Unauthorized();
        }
        
        var tokenString = GenerateJwtToken(user.Id, user.Email);
        
        return Ok (new { Token = tokenString });
    }

    private static string GenerateJwtToken(int id, string email)
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, email)
            }),
            Expires = DateTime.UtcNow.AddDays(28),
            Issuer = "spendo_be",
            Audience = "spendo_api",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var tokenHander = new JwtSecurityTokenHandler();
        var token = tokenHander.CreateToken(tokenDescriptor);
        var tokenString = tokenHander.WriteToken(token);
        return tokenString;
    }
}