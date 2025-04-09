using System;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services;

public class StockRequestService : IStockRequestRepository
{
    public Task<StockRequest> CreateStockRequestAsync(StockRequest stockRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteStockRequestAsync(int stockRequestId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StockRequest>> GetStockRequestsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<StockRequest?> GetStockRequestWithDetailsAsync(int stockRequestId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> StockRequestExistsAsync(int stockRequestId)
    {
        throw new NotImplementedException();
    }

    public Task<StockRequest> UpdateStockRequestAsync(StockRequest stockRequest)
    {
        throw new NotImplementedException();
    }
}

