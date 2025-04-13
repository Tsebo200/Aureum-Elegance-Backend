using System;
using Mystefy.Models;

namespace Mystefy.DTOs
{
    public class DeliveryDTO
    {
        public int DeliveryID { get; set; }
        public int SupplierID { get; set; }
        public DateTime DeliveryDateArrived { get; set; }
        public DateTime DeliveryDateOrdered { get; set; }
        public decimal DeliveryCost { get; set; }
        public int WarehouseID { get; set; }
    }
    public static class DeliveryMapper
    {
        public static DeliveryDTO MapToDTO(Delivery delivery)
        {
            return new DeliveryDTO
            {
                DeliveryID = delivery.DeliveryID,
                SupplierID = delivery.SupplierID,
                DeliveryDateArrived = delivery.DeliveryDateArrived,
                DeliveryDateOrdered = delivery.DeliveryDateOrdered,
                DeliveryCost = delivery.DeliveryCost,
                WarehouseID = delivery.WarehouseID
            };
        }

        public static Delivery MapToEntity(DeliveryDTO deliveryDto)
        {
            return new Delivery
            {
                DeliveryID = deliveryDto.DeliveryID,
                SupplierID = deliveryDto.SupplierID,
                DeliveryDateArrived = deliveryDto.DeliveryDateArrived,
                DeliveryDateOrdered = deliveryDto.DeliveryDateOrdered,
                DeliveryCost = deliveryDto.DeliveryCost,
                WarehouseID = deliveryDto.WarehouseID
            };
        }
    }
    // This DTO class is used to transfer data between the client and server, ensuring that only the necessary fields are included in the API responses. 
    // The mapper methods convert between the DTO and the entity model, allowing for easy data manipulation and transfer.
}

