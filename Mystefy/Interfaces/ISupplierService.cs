using System;
using Mystefy.DTOs;

namespace Mystefy.Interfaces;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDTO>> GetAllAsync();
    Task<SupplierDTO?> GetByIdAsync(int id);  // Make this nullable
    Task<SupplierDTO> AddAsync(SupplierDTO supplierDto);
    Task<SupplierDTO?> UpdateAsync(int id, SupplierDTO supplierDto);  // Make this nullable
    Task<bool> DeleteAsync(int id);
}
