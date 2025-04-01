using System.Collections.Generic;
using System.Threading.Tasks;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IStockRequestRepository
    {
        Task<IEnumerable<StockRequest>> GetStockRequestsAsync();
        Task<StockRequest?> GetStockRequestWithDetailsAsync(int stockRequestId);
        Task<StockRequest> CreateStockRequestAsync(StockRequest stockRequest);
        Task<StockRequest> UpdateStockRequestAsync(StockRequest stockRequest);
        Task<bool> DeleteStockRequestAsync(int stockRequestId);
        Task<bool> StockRequestExistsAsync(int stockRequestId);
    }
}
