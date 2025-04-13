using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IStockRequestPackagingsRepository
    {
        Task<List<StockRequestPackagings>> GetAllStockRequestPackagingsAsync();
        Task<StockRequestPackagings?> GetStockRequestPackagingsByIdAsync(int requestId);
        Task<StockRequestPackagings> CreateStockRequestPackagingsAsync(StockRequestPackagings request);
        Task<StockRequestPackagings?> UpdateStockRequestPackagingsAsync(StockRequestPackagings request);
        Task<StockRequestPackagings?> DeleteStockRequestPackagingsAsync(int requestId);

        Task<List<StockRequestPackagings>> GetRequestsByUserIdAsync(int userId);
        Task<List<StockRequestPackagings>> GetRequestsByWarehouseIdAsync(int warehouseId);
        Task<List<StockRequestPackagings>> GetRequestsByPackagingIdAsync(int packagingId);
        Task<List<StockRequestPackagings>> GetRequestsByStatusAsync(StockRequestPackagings.StockPackagingStatus status);
    }
}
