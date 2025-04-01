using System;
using Mystefy.Models;

namespace Mystefy.Interfaces;

public interface IStockRequestRepository
{

    Task<StockRequest> CreateStockRequestAsync(StockRequest stockRequest);
    Task<StockRequest?> GetStockRequestWithDetailsAsync(int stockRequest);

    Task<StockRequest> UpdateStockRequestAsync(StockRequest stockRequest);



}
