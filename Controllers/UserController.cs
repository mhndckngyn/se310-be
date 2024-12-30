using Microsoft.AspNetCore.Mvc;
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
        var user = _userService.GetUser(userId);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Post([FromBody] UserDto userInfo)
    {
        var user = _userService.Create(userInfo);
        return CreatedAtAction(nameof(Get), new { userId = user.Id }, user);
    }
}