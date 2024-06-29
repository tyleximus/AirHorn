namespace AirHorn.Api.Controllers;

using AirHorn.Api.Classes;
using AirHorn.Shared.Models;
using Microsoft.AspNetCore.Mvc;

[Route("/")]
[ApiController]
public class TransactionController : ControllerBase
{
  private readonly DataContext _dataContext;

  public TransactionController(DataContext dataContext)
  {
    _dataContext = dataContext;
  }

  [HttpPost("transactions")]
  public async Task<ActionResult<Transaction>> CreateTransaction(Transaction transaction)
  {
    try
    {
      if (transaction == null)
      {
        return BadRequest();
      }
      /// hypothetical if checking against unique keys
      //var duplicate = _dataContext.GetName(transaction.Name);
      //if (duplicate != null)
      //{
      //  ModelState.AddModelError("name", "Transaction name already in use");
      //  return BadRequest(ModelState);
      //}
      var createdTransaction = await _dataContext.CreateTransaction(transaction);
      return CreatedAtAction(nameof(GetTransaction), new { id = createdTransaction.TransactionID }, createdTransaction);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new transaction in database");
    }
  }

  [HttpDelete("transactions/{id}")]
  public async Task<ActionResult<Transaction>> DeleteTransaction(int id)
  {
    try
    {
      var transaction = await _dataContext.GetTransaction(id);
      if (transaction == null)
      {
        return NotFound($"Transaction with ID {id} not found.");
      }
      return await _dataContext.DeleteTransaction(id);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
    }
  }

  [HttpGet("transactions/{id}")]
  public async Task<ActionResult<Transaction>> GetTransaction(int id)
  {
    try
    {
      var result = await _dataContext.GetTransaction(id);
      if (result == null)
      {
        return NotFound();
      }
      return result;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
    }
  }

  [HttpGet("transactions")]
  public async Task<ActionResult> GetTransactions()
  {
    try
    {
      return Ok(await _dataContext.GetTransactions());
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
    }
  }

  [HttpPut("transactions/{id}")]
  public async Task<ActionResult<Transaction>> UpdateTransaction(int id, Transaction transactionRequest)
  {
    try
    {
      if (id != transactionRequest.TransactionID)
      {
        return BadRequest("Transaction ID mismatch");
      }
      var transaction = await _dataContext.GetTransaction(id);
      if (transaction == null)
      {
        return NotFound($"Transaction with ID {id} not found");
      }
      return await _dataContext.UpdateTransaction(transactionRequest);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
    }
  }
}
