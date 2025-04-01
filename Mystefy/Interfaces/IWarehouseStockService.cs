using System;
using Mystefy.Models;

namespace Mystefy.Interfaces;

public interface IWarehouseStockService
{
    Task<IEnumerable<WarehouseStock>> GetAllStockAsync();
        Task<WarehouseStock?> GetStockByIdAsync(int id);
        Task<WarehouseStock> AddStockAsync(WarehouseStock warehouseStock);
        Task<WarehouseStock?> UpdateStockAsync(int id, WarehouseStock warehouseStock);
        Task<bool> DeleteStockAsync(int id);

}
