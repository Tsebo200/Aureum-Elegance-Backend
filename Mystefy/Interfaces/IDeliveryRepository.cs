using System.Collections.Generic;
using System.Threading.Tasks;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    /// <summary>
    /// Defines a contract for handling delivery-related database operations.
    /// </summary>
    public interface IDeliveryRepository
    {
        /// <summary>
        /// Creates a new delivery record asynchronously.
        /// </summary>
        Task<Delivery> CreateDeliveryAsync(Delivery delivery);

        /// <summary>
        /// Retrieves a delivery by its ID asynchronously.
        /// </summary>
        Task<Delivery?> GetDeliveryByIdAsync(int deliveryId);

        /// <summary>
        /// Retrieves all delivery records asynchronously.
        /// </summary>
        Task<List<Delivery>> GetAllDeliveriesAsync();

        /// <summary>
        /// Updates an existing delivery record asynchronously.
        /// </summary>
        Task<Delivery?> UpdateDeliveryAsync(Delivery delivery);

        /// <summary>
        /// Deletes a delivery record by its ID asynchronously.
        /// Returns true if deleted successfully; otherwise, false.
        /// </summary>
        Task<bool> DeleteDeliveryAsync(int deliveryId);
    }
}
// This interface defines the contract for delivery-related operations, including creating, retrieving, updating, and deleting deliveries. 
//It also includes a method to retrieve all deliveries.
