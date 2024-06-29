namespace AirHorn.Shared.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

[Table("Transaction")]
public class Transaction
{
  [Key]
  public int TransactionID { get; set; }
  public string Name { get; set; }
  public DateOnly Date { get; set; }
  public Decimal Amount { get; set; }
  public string Note { get; set; }

  public Transaction Clone()
  {
    return new Transaction()
    {
      TransactionID = TransactionID,
      Name = Name,
      Date = Date,
      Amount = Amount,
      Note = Note
    };
  }
}
