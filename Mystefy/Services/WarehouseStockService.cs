using System;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services;

public class WarehouseStockService : IWarehouseStockService
{
     private readonly MystefyDbContext _context;

        public WarehouseStockService(MystefyDbContext context)
        {
            _context = context;
        }
    public async Task<WarehouseStock> AddStockAsync(WarehouseStock warehouseStock)
    {
       _context.WarehouseStocks.Add(warehouseStock);
            await _context.SaveChangesAsync();
            return warehouseStock;}

    public async Task<bool> DeleteStockAsync(int id)
    {
        var stock = await _context.WarehouseStocks.FindAsync(id);
            if (stock == null) return false;

            _context.WarehouseStocks.Remove(stock);
            await _context.SaveChangesAsync();
            return true;;
    }

    public async Task<IEnumerable<WarehouseStock>> GetAllStockAsync()
    {
        
            return await _context.WarehouseStocks
            .Include(ws => ws.Fragrance)
            .Include(ws => ws.Warehouse)
            .ToListAsync();
    }

    public async Task<WarehouseStock?> GetStockByIdAsync(int id)
    {
        return await _context.WarehouseStocks
        .Include(ws => ws.Fragrance)
        .Include(ws => ws.Warehouse)
        .FirstOrDefaultAsync(ws => ws.StockID == id);
    }

    public async Task<WarehouseStock?> UpdateStockAsync(int id, WarehouseStock warehouseStock)
    {
          var existingStock = await _context.WarehouseStocks.FindAsync(id);
            if (existingStock == null) return null;

            existingStock.Stock = warehouseStock.Stock;
            existingStock.FragranceID = warehouseStock.FragranceID;
            existingStock.WarehouseID = warehouseStock.WarehouseID;

            await _context.SaveChangesAsync();
            return existingStock;
    }
}
