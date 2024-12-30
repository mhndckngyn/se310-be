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
    [HttpGet]
    public IActionResult GetAccounts()
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized("Invalid or missing user ID.");
        }
        var accounts = _accountService.GetAccounts(userId);
        return Ok(accounts);
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateAccount([FromBody] string accountName)
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized("Invalid or missing user ID.");
        }
        var account = _accountService.CreateAccount(accountName);
        return Ok(account);
    }

    [Authorize]
    [HttpPut]
    public IActionResult UpdateAccount([FromBody] AccountUpdateDto newAccountInfo)
    {
        var account = _accountService.UpdateAccount(newAccountInfo);
        return Ok(account);
    }

    [Authorize]
    [HttpDelete("{accountId:int}")]
    public IActionResult DeleteAccount(int accountId)
    {
        _accountService.DeleteAccount(accountId);
        return NoContent();
    }
}