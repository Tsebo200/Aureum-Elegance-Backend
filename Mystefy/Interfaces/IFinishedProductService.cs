using System.Collections.Generic;
using System.Threading.Tasks;
using Mystefy.DTOs;

namespace Mystefy.Interfaces
{
    public interface IFinishedProductService
    {
        Task<IEnumerable<FinishedProductDTO>> GetAllProductsAsync();
        Task<FinishedProductDTO?> GetProductByIdAsync(int productId);
        Task<bool> CreateProductAsync(FinishedProductDTO product);
        Task<bool> UpdateProductAsync(FinishedProductDTO product);
        Task<bool> DeleteProductAsync(int productId);
    }
}
