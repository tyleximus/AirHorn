namespace AirHorn.Api.Classes;

using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using AirHorn.Shared.Models;
using System.Text;
using System.Runtime.InteropServices;
using System.Linq;

public class DataContext : DbContext
{
  protected readonly IConfiguration _configuration;

  public DataContext(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(_configuration.GetConnectionString("OnPrem"));
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }

  public DbSet<Transaction> Transactions { get; set; }

  public async Task<Transaction> CreateTransaction(Transaction transaction)
  {
    var result = await Transactions.AddAsync(transaction);
    await SaveChangesAsync();
    return result.Entity;
  }

  public async Task<Transaction> DeleteTransaction(int transactionID)
  {
    var result = await (from t in Transactions select t)
      .Where(t => t.TransactionID == transactionID)
      .AsNoTracking()
      .ToListAsync();
    if (result != null)
    {
      var transaction = result.First();
      Transactions.Remove(transaction);
      await SaveChangesAsync();
      return transaction;
    }
    return null;
  }

  public async Task<Transaction> GetTransaction(int transactionID)
  {
    var result = await (from t in Transactions select t)
        .Where(t => t.TransactionID == transactionID)
        .AsNoTracking()
        .ToListAsync();
    return result.FirstOrDefault();
  }

  public async Task<IEnumerable<Transaction>> GetTransactions()
  {
    return await (from t in Transactions select t)
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<Transaction> UpdateTransaction(Transaction transactionRequest)
  {
    var result = await(from t in Transactions select t)
      .Where(t => t.TransactionID == transactionRequest.TransactionID)
      .AsNoTracking()
      .ToListAsync();
    if (result != null)
    {
      var transaction = result.FirstOrDefault();
      transaction.Name = transactionRequest.Name;
      transaction.Date = transactionRequest.Date;
      transaction.Amount = transactionRequest.Amount;
      transaction.Note = transactionRequest.Note;
      Transactions.Update(transaction);
      await SaveChangesAsync();
      return transaction;
    }
    return null;
  }
}
