using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly MystefyDbContext _context;

        public SupplierService(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupplierDTO>> GetAllAsync()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return suppliers.Select(s => ToDto(s)).ToList();
        }

        public async Task<SupplierDTO?> GetByIdAsync(int id)  // Nullable return type
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            return supplier == null ? null : ToDto(supplier);  // Return null if not found
        }

        public async Task<SupplierDTO> AddAsync(SupplierDTO supplierDto)
        {
            var supplier = FromDto(supplierDto);
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return ToDto(supplier);
        }

        public async Task<SupplierDTO?> UpdateAsync(int id, SupplierDTO supplierDto)  // Nullable return type
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return null;

            supplier.SupplierName = supplierDto.SupplierName ?? supplier.SupplierName;  // Handle null values
            supplier.ContactPerson = supplierDto.ContactPerson ?? supplier.ContactPerson;
            supplier.PhoneNumber = supplierDto.PhoneNumber ?? supplier.PhoneNumber;

            await _context.SaveChangesAsync();
            return ToDto(supplier);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return false;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping methods
        private static SupplierDTO ToDto(Supplier supplier) => new SupplierDTO
        {
            SupplierID = supplier.SupplierID,
            SupplierName = supplier.SupplierName,
            ContactPerson = supplier.ContactPerson,
            PhoneNumber = supplier.PhoneNumber
        };

        private static Supplier FromDto(SupplierDTO dto) => new Supplier
        {
            SupplierID = dto.SupplierID,
            SupplierName = dto.SupplierName ?? "",  // Handle null values
            ContactPerson = dto.ContactPerson ?? "",  // Handle null values
            PhoneNumber = dto.PhoneNumber ?? ""  // Handle null values
        };
    }
}
