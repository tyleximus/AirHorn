namespace AirHorn.App.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using AirHorn.Shared.Models;

public interface IAirHornService
{
  Task<Transaction> CreateTransaction(Transaction request);
  Task<Transaction> DeleteTransaction(int id);
  Task<Transaction> GetTransaction(int id);
  Task<IEnumerable<Transaction>> GetTransactions();
  Task<Transaction> UpdateTransaction(int id, Transaction request);
}

public class AirHornService : IAirHornService
{
  private IHttpService _httpService;

  public AirHornService(IHttpService httpService)
  {
    _httpService = httpService;
  }

  public async Task<Transaction> CreateTransaction(Transaction request)
  {
    return await _httpService.Post<Transaction>("transactions", request);
  }

  public async Task<Transaction> DeleteTransaction(int id)
  {
    return await _httpService.Delete<Transaction>($"transactions/{id}");
  }

  public async Task<Transaction> GetTransaction(int id)
  {
    return await _httpService.Get<Transaction>($"transactions/{id}");
  }

  public async Task<IEnumerable<Transaction>> GetTransactions()
  {
    return await _httpService.Get<IEnumerable<Transaction>>($"transactions");
  }

  public async Task<Transaction> UpdateTransaction(int id, Transaction request)
  {
    return await _httpService.Put<Transaction>($"transactions/{id}", request);
  }

  //public async Task<IEnumerable<ProgressTime>> GetProgressTimes(int athleteID)
  //{
  //    return await _httpService.Get<IEnumerable<ProgressTime>>($"progress-times/{athleteID}");
  //}
}