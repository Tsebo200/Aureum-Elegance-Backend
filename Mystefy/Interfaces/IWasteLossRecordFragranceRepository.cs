using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IWasteLossRecordFragranceRepository
    {
        Task<List<WasteLossRecordFragrance>> GetAllWasteLossRecordsAsync();
        Task<WasteLossRecordFragrance?> GetWasteLossRecordByIdAsync(int recordId);
        Task<WasteLossRecordFragrance> CreateWasteLossRecordAsync(WasteLossRecordFragrance record);
        Task<WasteLossRecordFragrance?> UpdateWasteLossRecordAsync(WasteLossRecordFragrance record);
        Task<WasteLossRecordFragrance?> DeleteWasteLossRecordAsync(int recordId);

        Task<List<WasteLossRecordFragrance>> GetWasteLossRecordsByUserIdAsync(int userId);
        Task<List<WasteLossRecordFragrance>> GetWasteLossRecordsByFragranceIdAsync(int fragranceId);
        Task<List<WasteLossRecordFragrance>> GetWasteLossRecordsByWarehouseIdAsync(int warehouseId);
    }
}
