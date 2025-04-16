using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IWasteLossRecordBatchFinishedProductsRepository
    {
        Task<List<WasteLossRecordBatchFinishedProducts>> GetAllWasteLossRecordsAsync();
        Task<WasteLossRecordBatchFinishedProducts?> GetWasteLossRecordByIdAsync(int recordId);
        Task<WasteLossRecordBatchFinishedProducts> CreateWasteLossRecordAsync(WasteLossRecordBatchFinishedProducts record);
        Task<WasteLossRecordBatchFinishedProducts?> UpdateWasteLossRecordAsync(WasteLossRecordBatchFinishedProducts record);
        Task<WasteLossRecordBatchFinishedProducts?> DeleteWasteLossRecordAsync(int recordId);

        Task<List<WasteLossRecordBatchFinishedProducts>> GetWasteLossRecordsByUserIdAsync(int userId);
        Task<List<WasteLossRecordBatchFinishedProducts>> GetWasteLossRecordsByProductIdAsync(int productId);
        Task<List<WasteLossRecordBatchFinishedProducts>> GetWasteLossRecordsByBatchIdAsync(int batchId);
        Task<List<WasteLossRecordBatchFinishedProducts>> GetWasteLossRecordsByWarehouseIdAsync(int warehouseId);
    }
}
