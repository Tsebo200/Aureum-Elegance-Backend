using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    public class PackagingRepositoryService : IPackagingRepository
    {
        private readonly MystefyDbContext _context;

        public PackagingRepositoryService(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<Packaging> CreatePackagingAsync(Packaging packaging)
        {
            _context.Packaging.Add(packaging);
            await _context.SaveChangesAsync();

            // Load related FinishedProduct entities if needed
            await _context.Entry(packaging)
                .Collection(p => p.FinishedProduct)
                .LoadAsync();

            return packaging;
        }

        public async Task<Packaging?> GetPackagingWithDetailsAsync(int packagingId)
        {
            return await _context.Packaging
                .Include(p => p.FinishedProduct)
                .FirstOrDefaultAsync(p => p.Id == packagingId);
        }

        public async Task<Packaging?> GetPackagingByNameAsync(string packagingName)
        {
            return await _context.Packaging
                .Include(p => p.FinishedProduct)
                .FirstOrDefaultAsync(p => p.Name == packagingName);
        }

        public async Task<Packaging?> GetPackagingByTypeAsync(string packagingType)
        {
            return await _context.Packaging
                .Include(p => p.FinishedProduct)
                .FirstOrDefaultAsync(p => p.Type == packagingType);
        }

        public async Task<Packaging?> UpdatePackagingAsync(Packaging packaging)
        {
            _context.Entry(packaging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Refresh the related FinishedProduct entities
                await _context.Entry(packaging)
                    .Collection(p => p.FinishedProduct)
                    .LoadAsync();

                return packaging;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PackagingExists(packaging.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Packaging?> DeletePackagingAsync(int packagingId)
        {
            var packaging = await _context.Packaging.FindAsync(packagingId);
            if (packaging == null)
            {
                return null;
            }

            _context.Packaging.Remove(packaging);
            await _context.SaveChangesAsync();

            return packaging;
        }

        // Manage the relationship with FinishedProduct

        public async Task AddFinishedProductToPackagingAsync(int packagingId, int finishedProductId)
        {
            // Retrieve Packaging with its FinishedProduct collection
            var packaging = await _context.Packaging
                .Include(p => p.FinishedProduct)
                .FirstOrDefaultAsync(p => p.Id == packagingId);

            if (packaging == null)
                throw new KeyNotFoundException("Packaging not found");

            // Retrieve the FinishedProduct from the database
            var finishedProduct = await _context.FinishedProduct.FindAsync(finishedProductId);
            if (finishedProduct == null)
                throw new KeyNotFoundException("Finished product not found");

            // Associate the finished product with the packaging
            finishedProduct.PackagingID = packagingId;
            packaging.FinishedProduct.Add(finishedProduct);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFinishedProductFromPackagingAsync(int packagingId, int finishedProductId)
        {
            var packaging = await _context.Packaging
                .Include(p => p.FinishedProduct)
                .FirstOrDefaultAsync(p => p.Id == packagingId);

            if (packaging == null)
                throw new KeyNotFoundException("Packaging not found");

            var finishedProduct = packaging.FinishedProduct.FirstOrDefault(fp => fp.ProductID == finishedProductId);
            if (finishedProduct == null)
                throw new KeyNotFoundException("Finished product not associated with this packaging");

            packaging.FinishedProduct.Remove(finishedProduct);
            await _context.SaveChangesAsync();
        }

        // Private helper method to check existence of packaging
        private async Task<bool> PackagingExists(int id)
        {
            return await _context.Packaging.AnyAsync(e => e.Id == id);
        }
    }
}
