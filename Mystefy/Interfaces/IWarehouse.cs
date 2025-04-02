using System;
using Mystefy.Models;

namespace Mystefy.Interfaces;

public interface IWarehouse
{
     Task<IEnumerable<Warehouse>> GetAllWarehouses();
       Task<IEnumerable<Warehouse>>GetAllWarehousesAndStockRequests();
       Task<IEnumerable<Warehouse>>GetAllWarehousesAndWarehouseStock();
        Task<Warehouse?> GetWarehouseById(int warehouseId);
        Task<Warehouse> MakeWarehouse(Warehouse warehouse);
        Task<bool> UpdateWarehouse(int warehouseId, Warehouse warehouse);
        Task<bool> DeleteWarehouse(int warehouseId);
}
