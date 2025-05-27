using System;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services;

public class FinishedProductPackagingService : IFinishedProductPackaging
{
    private readonly MystefyDbContext _context;

        public FinishedProductPackagingService(MystefyDbContext context)
        {
            _context = context;
        }

    public async Task<FinishedProductPackaging> AddFinishedProductPackaging(FinishedProductPackaging finishedProductPackaging)
    {
         _context.FinishedProductPackaging.Add(finishedProductPackaging);
            await _context.SaveChangesAsync();
            return finishedProductPackaging;
    }

    public async Task<bool> DeleteFinishedProductPackaging(int ProductID, int PackagingId)
    {
        var finishedProductPackage = await _context.FinishedProductPackaging
                .FirstOrDefaultAsync(fpp => fpp.ProductID == ProductID && fpp.PackagingID == PackagingId);

            if (finishedProductPackage == null) return false;

            _context.FinishedProductPackaging.Remove(finishedProductPackage);
            await _context.SaveChangesAsync();
            return true;
    }

    public async Task<IEnumerable<FinishedProductPackaging>> GetAllFinishedProductPackaging()
    {
          return await _context.FinishedProductPackaging
                .Include(fpp => fpp.FinishedProduct)
                .Include(fpp => fpp.Packaging)
                .ToListAsync();
    }

    public async Task<FinishedProductPackaging?> GetFinishedProductPackagingById(int ProductID, int PackagingId)
    {
         return await _context.FinishedProductPackaging
                .Include(fpp => fpp.FinishedProduct)
                .Include(fpp => fpp.Packaging)
                .FirstOrDefaultAsync(fpp => fpp.ProductID == ProductID && fpp.PackagingID == PackagingId);
    }

    public async Task<bool> UpdateFinishedProductPackaging(int ProductID, int PackagingId, FinishedProductPackaging updateFinishedProductPackaging)
    {
        var existing = await _context.FinishedProductPackaging
                .FirstOrDefaultAsync(fpp => fpp.ProductID == ProductID && fpp.PackagingID == PackagingId);

            if (existing == null) return false;

            // Update properties
            existing.Amount = updateFinishedProductPackaging.Amount;
            existing.ProductID= updateFinishedProductPackaging.ProductID;
            existing.PackagingID = updateFinishedProductPackaging.PackagingID;

            _context.Entry(existing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
    }
}
