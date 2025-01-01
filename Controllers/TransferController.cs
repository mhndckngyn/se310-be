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

    [HttpGet("{id:int}")]
    [Authorize]
    public IActionResult GetTransferById(int id)
    {
        var transfer = _transferService.GetTransferById(id);
        if (transfer == null)
        {
            return NotFound();
        }
        
        return Ok(transfer);
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
            return Unauthorized();
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
        var transfer = _transferService.CreateTransfer(transferInfo);
        return CreatedAtAction(nameof(GetTransferById), new { id = transfer.Id}, transfer);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public IActionResult UpdateTransfer([FromRoute] int id, [FromBody] TransferUpdateDto transferInfo)
    {
        var updatedTransfer = _transferService.UpdateTransfer(id, transferInfo);
        if (updatedTransfer == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DeleteTransfer([FromRoute] int id)
    {
        var deletedTransfer = _transferService.DeleteTransfer(id);

        if (deletedTransfer == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}