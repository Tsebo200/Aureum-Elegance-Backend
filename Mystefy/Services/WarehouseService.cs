using System;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services;

public class WarehouseService : IWarehouse
{
    private readonly MystefyDbContext _context;

        public WarehouseService(MystefyDbContext context)
        {
            _context = context;
        }
    public async Task<Warehouse> MakeWarehouse(Warehouse warehouse)
    {
        _context.Warehouses.Add(warehouse);
        await _context.SaveChangesAsync();
        return warehouse;
    }

    public async Task<bool> DeleteWarehouse(int warehouseId)
    {
          var warehouse = await _context.Warehouses.FindAsync(warehouseId);
            if (warehouse == null)
            {
                return false;
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return true;
    }

    public async Task<IEnumerable<Warehouse>> GetAllWarehouses()
    {
        return await _context.Warehouses.ToListAsync();
    }

    public async Task<Warehouse?> GetWarehouseById(int warehouseId)
    {
        return await _context.Warehouses.FindAsync(warehouseId);
    }

    public async Task<bool> UpdateWarehouse(int warehouseId, Warehouse warehouse)
    {
        if (warehouseId != warehouse.WarehouseID)
            {
                return false;
            }

            _context.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(warehouseId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            
    }
     private bool WarehouseExists(int warehouseId)
        {
            return _context.Warehouses.Any(e => e.WarehouseID == warehouseId);
        }
}
