using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IStockRequestRepository
    {
        Task<List<StockRequest>> GetAllStockRequestsAsync();
        Task<StockRequest> CreateStockRequestAsync(StockRequest request);
        Task<StockRequest?> GetStockRequestByIdAsync(int requestId);
        Task<List<StockRequest>> GetRequestsByUserIdAsync(int userId);
        Task<List<StockRequest>> GetRequestsByWarehouseIdAsync(int warehouseId);
        Task<List<StockRequest>> GetRequestsByIngredientIdAsync(int ingredientId);
        Task<List<StockRequest>> GetRequestsByStatusAsync(string status);
        Task<StockRequest?> UpdateStockRequestAsync(StockRequest request);
        Task<StockRequest?> DeleteStockRequestAsync(int requestId);
    }
}
