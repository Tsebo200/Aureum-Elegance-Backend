using System;
using Mystefy.Models;

namespace Mystefy.Interfaces;

public interface IWarehouse
{
     Task<IEnumerable<Warehouse>> GetAllWarehouses();
       
       Task<IEnumerable<Warehouse>>GetAllWarehousesAndWarehouseStock();

       Task<Warehouse?> GetWasteLossIngredientsByWarehouseId(int warehouseId);
       Task<Warehouse?> GetWasteLossPackagingByWarehouseId(int warehouseId);
       Task<Warehouse?> GetWasteLossFragranceByWarehouseId(int warehouseId);
       Task<Warehouse?> GetWasteLossBatchFinishedProductsByWarehouseId(int warehouseId);


        Task<Warehouse?> GetWarehouseById(int warehouseId);
        Task<Warehouse> MakeWarehouse(Warehouse warehouse);
        Task<bool> UpdateWarehouse(int warehouseId, Warehouse warehouse);
        Task<bool> DeleteWarehouse(int warehouseId);
}
