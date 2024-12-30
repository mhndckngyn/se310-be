using Microsoft.AspNetCore.Mvc;
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
}