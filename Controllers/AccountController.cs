using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spendo_be.Models.DTO;
using spendo_be.Services;

namespace spendo_be.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetAccount([FromRoute] int id)
    {
        var account = _accountService.GetAccountById(id);

        if (account == null)
        {
            return NotFound();
        }
        
        return Ok(account);
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult GetAccounts()
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized();
        }
        
        var accounts = _accountService.GetAccountsByUser(userId);
        return Ok(accounts);
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateAccount([FromBody] string accountName)
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized();
        }
        
        var account = _accountService.CreateAccount(userId, accountName);
        return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult UpdateAccount([FromRoute] int id, [FromBody] string accountName)
    {
        var updatedAccount = _accountService.UpdateAccount(id, accountName);
        if (updatedAccount == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAccount([FromRoute] int id)
    {
        var deletedAccount = _accountService.DeleteAccount(id);
        if (deletedAccount == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}