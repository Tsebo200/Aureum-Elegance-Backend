using System;
using Mystefy.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mystefy.Interfaces
{
    public interface IBatchFinishedProductService
    {
        Task<IEnumerable<BatchFinishedProductDTO>> GetAllAsync();
        Task<BatchFinishedProductDTO?> GetByIdAsync(int batchId, int productId);
        Task<bool> CreateAsync(BatchFinishedProductDTO dto);
        Task<bool> UpdateAsync(BatchFinishedProductDTO dto);
        Task<bool> DeleteAsync(int batchId, int productId);
    }
}
