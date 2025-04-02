using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mystefy.DTOs;
using Mystefy.Interfaces;

namespace Mystefy.Services
{
    public class FinishedProductService : IFinishedProductService
    {
        private readonly List<FinishedProductDTO> _products = new();

        public async Task<IEnumerable<FinishedProductDTO>> GetAllProductsAsync()
        {
            return await Task.FromResult(_products);
        }

        public async Task<FinishedProductDTO?> GetProductByIdAsync(int productId)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == productId);
            return await Task.FromResult(product);
        }

        public async Task<bool> CreateProductAsync(FinishedProductDTO product)
        {
            _products.Add(product);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateProductAsync(FinishedProductDTO product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (existingProduct == null)
                return await Task.FromResult(false);

            existingProduct.FragranceID = product.FragranceID;
            existingProduct.PackagingID = product.PackagingID;
            existingProduct.Quantity = product.Quantity;

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
                return await Task.FromResult(false);

            _products.Remove(product);
            return await Task.FromResult(true);
        }
    }
}
