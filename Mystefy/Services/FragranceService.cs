using System;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services;

public class FragranceService : IFragranceService
{
     private readonly MystefyDbContext _context;

        public FragranceService(MystefyDbContext context)
        {
            _context = context;
        }
    public async Task<Fragrance> AddFragrance(Fragrance fragrance)
    {
        fragrance.ExpiryDate = DateTime.SpecifyKind(fragrance.ExpiryDate, DateTimeKind.Utc);
            _context.Fragrances.Add(fragrance);
            await _context.SaveChangesAsync();
            return fragrance;
    }

    public async Task<bool> DeleteFragrance(int id)
    {
        var fragrance = await _context.Fragrances.FindAsync(id);
            if (fragrance == null) return false;

            _context.Fragrances.Remove(fragrance);
            await _context.SaveChangesAsync();
            return true;
    }

    public async Task<IEnumerable<Fragrance>> GetAllFragrances()
    {
        return await _context.Fragrances
        .Include(f => f.FragranceIngredients)  
        .ThenInclude(fi => fi.Ingredients)
        .ToListAsync();
    }

    public async Task<Fragrance?> GetFragranceById(int id)
    {
        return await _context.Fragrances
        .Include(f => f.FragranceIngredients)  
        .ThenInclude(fi => fi.Ingredients)
        .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<bool> UpdateFragrance(int id, Fragrance fragrance)
    {
          if (id != fragrance.Id) return false;

            _context.Entry(fragrance).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return _context.Fragrances.Any(e => e.Id == id);
            }
    }
}
