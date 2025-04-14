using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IWasteLossRecordIngredientsRepository
    {
        Task<List<WasteLossRecordIngredients>> GetAllWasteLossRecordsAsync();
        Task<WasteLossRecordIngredients?> GetWasteLossRecordByIdAsync(int recordId);
        Task<WasteLossRecordIngredients> CreateWasteLossRecordAsync(WasteLossRecordIngredients record);
        Task<WasteLossRecordIngredients?> UpdateWasteLossRecordAsync(WasteLossRecordIngredients record);
        Task<WasteLossRecordIngredients?> DeleteWasteLossRecordAsync(int recordId);

        Task<List<WasteLossRecordIngredients>> GetWasteLossRecordsByUserIdAsync(int userId);
        Task<List<WasteLossRecordIngredients>> GetWasteLossRecordsByIngredientIdAsync(int ingredientId);
        Task<List<WasteLossRecordIngredients>> GetWasteLossRecordsByWarehouseIdAsync(int warehouseId);
    }
}
