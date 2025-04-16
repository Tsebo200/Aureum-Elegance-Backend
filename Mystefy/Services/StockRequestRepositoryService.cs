using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;
using Microsoft.EntityFrameworkCore;

namespace Mystefy.Services
{
    public class StockRequestRepositoryService : IStockRequestRepository
    {
        private readonly MystefyDbContext _context;

        public StockRequestRepositoryService(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockRequest>> GetAllStockRequestsAsync()
        {
            return await _context.StockRequests
                                 .Include(r => r.User)
                                 .Include(r => r.Warehouse)
                                 .Include(r => r.Ingredients)
                                 .ToListAsync();
        }

        public async Task<StockRequest?> GetStockRequestByIdAsync(int requestId)
        {
            return await _context.StockRequests
                                 .Include(r => r.User)
                                 .Include(r => r.Warehouse)
                                 .Include(r => r.Ingredients)
                                 .FirstOrDefaultAsync(r => r.Id == requestId);
        }

        public async Task<List<StockRequest>> GetRequestsByUserIdAsync(int userId)
        {
            return await _context.StockRequests
                                 .Where(r => r.UserId == userId)
                                 .Include(r => r.User)
                                 .Include(r => r.Warehouse)
                                 .Include(r => r.Ingredients)
                                 .ToListAsync();
        }

        public async Task<List<StockRequest>> GetRequestsByWarehouseIdAsync(int warehouseId)
        {
            return await _context.StockRequests
                                 .Where(r => r.WarehouseId == warehouseId)
                                 .Include(r => r.User)
                                 .Include(r => r.Warehouse)
                                 .Include(r => r.Ingredients)
                                 .ToListAsync();
        }

        public async Task<List<StockRequest>> GetRequestsByIngredientIdAsync(int ingredientId)
        {
            return await _context.StockRequests
                                 .Where(r => r.IngredientsId == ingredientId)
                                 .Include(r => r.User)
                                 .Include(r => r.Warehouse)
                                 .Include(r => r.Ingredients)
                                 .ToListAsync();
        }

        public async Task<List<StockRequest>> GetRequestsByStatusAsync(string status)
        {
            return await _context.StockRequests
                                 .Where(r => r.Status == status)
                                 .Include(r => r.User)
                                 .Include(r => r.Warehouse)
                                 .Include(r => r.Ingredients)
                                 .ToListAsync();
        }

        public async Task<StockRequest> CreateStockRequestAsync(StockRequest request)
        {
            _context.StockRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<StockRequest?> UpdateStockRequestAsync(StockRequest request)
        {
            var existingRequest = await _context.StockRequests.FindAsync(request.Id);
            if (existingRequest == null) return null;

            existingRequest.AmountRequested = request.AmountRequested;
            existingRequest.Status = request.Status;
            existingRequest.RequestDate = request.RequestDate;
            existingRequest.IngredientsId = request.IngredientsId;
            existingRequest.UserId = request.UserId;
            existingRequest.WarehouseId = request.WarehouseId;

            await _context.SaveChangesAsync();
            return existingRequest;
        }

        public async Task<StockRequest?> DeleteStockRequestAsync(int requestId)
        {
            var request = await _context.StockRequests.FindAsync(requestId);
            if (request == null) return null;

            _context.StockRequests.Remove(request);
            await _context.SaveChangesAsync();
            return request;
        }
    }
}
