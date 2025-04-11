using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IPackagingRepository
    {
        Task<List<Packaging>> GetAllPackagingAsync();
        Task<Packaging> CreatePackagingAsync(Packaging packaging);
        Task<Packaging?> GetPackagingWithDetailsAsync(int packagingId);
        Task<Packaging?> GetPackagingByNameAsync(string packagingName);
        Task<Packaging?> GetPackagingByTypeAsync(string packagingType);
        Task<Packaging?> UpdatePackagingAsync(Packaging packaging);
        Task<Packaging?> DeletePackagingAsync(int packagingId);

        // Methods to manage the connection with FinishedProduct
        Task AddFinishedProductToPackagingAsync(int packagingId, int finishedProductId);
        Task RemoveFinishedProductFromPackagingAsync(int packagingId, int finishedProductId);
    }
}


