using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mystefy.Services
{
    public class FinishedProductService : IFinishedProductService
    {
        private readonly MystefyDbContext _context;

        public FinishedProductService(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<FinishedProduct?> CreateProductAsync(FinishedProduct finishedProduct)
        {
            _context.FinishedProduct.Add(finishedProduct);
            await _context.SaveChangesAsync();
            return finishedProduct;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.FinishedProduct.FindAsync(productId);
            if (product == null)
                return false;

            _context.FinishedProduct.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<FinishedProduct>> GetAllProductsAsync()
        {
            return await _context.FinishedProduct
                .Include(fp => fp.Fragrance)
                .Include(fp => fp.FinishedProductPackaging) 
                   .ThenInclude(fpp => fpp.Packaging)
                .ToListAsync();
        }

        public async Task<FinishedProduct?> GetFinishedProductByName(string name)
        {
            return await _context.FinishedProduct
                .FirstOrDefaultAsync(p => p.ProductName.ToLower() == name.ToLower());
        }

        public async Task<FinishedProduct?> GetProductByIdAsync(int productId)
        {
            return await _context.FinishedProduct
                .Include(fp => fp.Fragrance)
                .Include(fp => fp.FinishedProductPackaging)
                   .ThenInclude(fpp => fpp.Packaging)
                .FirstOrDefaultAsync(fp => fp.ProductID == productId);
        }

        public async Task<bool> UpdateProductAsync(FinishedProduct finishedProduct)
        {
            var existingProduct = await _context.FinishedProduct.FindAsync(finishedProduct.ProductID);
            if (existingProduct == null)
                return false;

            _context.Entry(existingProduct).CurrentValues.SetValues(finishedProduct);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
