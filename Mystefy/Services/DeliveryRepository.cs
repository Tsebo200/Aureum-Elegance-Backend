using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly MystefyDbContext _context;

        public DeliveryRepository(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<Delivery> CreateDeliveryAsync(Delivery delivery)
        {
            var newDelivery = await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
            return newDelivery.Entity;
        }

        public async Task<Delivery?> GetDeliveryByIdAsync(int deliveryId)
        {
            return await _context.Deliveries
                .Include(d => d.DeliveryIngredients)
                //.Include(d => d.DeliveryPackaging)
                .FirstOrDefaultAsync(d => d.DeliveryID == deliveryId);
        }

        public async Task<List<Delivery>> GetAllDeliveriesAsync()
        {
            return await _context.Deliveries
                .Include(d => d.DeliveryIngredients)
                //.Include(d => d.DeliveryPackaging)
                .ToListAsync();
        }

        public async Task<Delivery?> UpdateDeliveryAsync(Delivery delivery)
        {
            var existing = await _context.Deliveries.FindAsync(delivery.DeliveryID);
            if (existing == null)
                return null;

            _context.Entry(existing).CurrentValues.SetValues(delivery);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteDeliveryAsync(int deliveryId)
        {
            var delivery = await _context.Deliveries.FindAsync(deliveryId);
            if (delivery == null)
                return false;

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

