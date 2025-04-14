using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IWasteLossRecordPackagingRepository
    {
        Task<List<WasteLossRecordPackaging>> GetAllWasteLossRecordsAsync();
        Task<WasteLossRecordPackaging?> GetWasteLossRecordByIdAsync(int recordId);
        Task<WasteLossRecordPackaging> CreateWasteLossRecordAsync(WasteLossRecordPackaging record);
        Task<WasteLossRecordPackaging?> UpdateWasteLossRecordAsync(WasteLossRecordPackaging record);
        Task<WasteLossRecordPackaging?> DeleteWasteLossRecordAsync(int recordId);

        Task<List<WasteLossRecordPackaging>> GetWasteLossRecordsByUserIdAsync(int userId);
        Task<List<WasteLossRecordPackaging>> GetWasteLossRecordsByPackagingIdAsync(int packagingId);
        Task<List<WasteLossRecordPackaging>> GetWasteLossRecordsByWarehouseIdAsync(int warehouseId);
    }
}
