using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spendo_be.Models.DTO;
using spendo_be.Services;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Controllers;

[Route("[controller]")]
[ApiController]
public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransferController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetTransfers(
        [FromQuery] string[] accountIds, 
        [FromQuery] string[] categoryIds, 
        [FromQuery] DateTime? startDate, 
        [FromQuery] DateTime? endDate)
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized("Invalid or missing user ID.");
        }
        
        var accountIdsAsInts = accountIds
            .Where(id => int.TryParse(id, out _))
            .Select(int.Parse)
            .ToArray();
        
        var categoryIdsAsInts = categoryIds
            .Where(id => int.TryParse(id, out _))
            .Select(int.Parse)
            .ToArray();

        var criteria = new RecordQueryCriteria
        {
            UserId = userId,
            AccountIds = accountIdsAsInts,
            CategoryIds = categoryIdsAsInts,
            StartDate = startDate,
            EndDate = endDate
        };
        
        var transfers = _transferService.GetListTransferByCriteria(criteria);
        return Ok(transfers);
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateTransfer([FromBody] TransferCreateDto transferInfo)
    {
        _transferService.CreateTransfer(transferInfo);
        return Ok();
    }

    [HttpPut]
    [Authorize]
    public IActionResult UpdateTransfer([FromBody] TransferUpdateDto transferInfo)
    {
        _transferService.UpdateTransfer(transferInfo);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DeleteTransfer([FromRoute] int id)
    {
        _transferService.DeleteTransfer(id);
        return Ok();
    }
}