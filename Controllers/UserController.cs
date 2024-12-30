using Microsoft.AspNetCore.Mvc;
using spendo_be.Models;
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
    public IActionResult Post([FromBody] string email)
    {
        var user = _userService.Create(email);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, null);
    }
}