using System.Collections.Generic;
using System.Threading.Tasks;
using Mystefy.DTOs;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IFinishedProductService
    {
        Task<IEnumerable<FinishedProduct>> GetAllProductsAsync();
        Task<FinishedProduct?> GetProductByIdAsync(int productId);
        Task<FinishedProduct?> CreateProductAsync(FinishedProduct finishedProduct);
        Task<bool> UpdateProductAsync(FinishedProduct finishedProduct);
        Task<bool> DeleteProductAsync(int productId);

        Task<FinishedProduct?> GetFinishedProductByName(string name);
    }
}
