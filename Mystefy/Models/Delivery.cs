using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    public class Delivery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeliveryID { get; set; }

        // Foreign key to Supplier. One supplier can have many deliveries.
        [ForeignKey(nameof(Supplier))]
        public int SupplierID { get; set; }

        // Date when the delivery arrived.
        public DateTime DeliveryDateArrived { get; set; }

        // Date when the delivery was ordered.
        public DateTime DeliveryDateOrdered { get; set; }

        // Cost of the delivery.
        public decimal DeliveryCost { get; set; }

        // Foreign key to Warehouse. One warehouse can have many deliveries.
        [ForeignKey(nameof(Warehouse))]
        public int WarehouseID { get; set; }

        // Navigation property: The supplier associated with this delivery.
        public Supplier Supplier { get; set; } = null!;

        // Navigation property: The warehouse associated with this delivery.
        public Warehouse Warehouse { get; set; } = null!;

        // Navigation property: A delivery can have multiple DeliveryIngredients.
        public List<DeliveryIngredients> DeliveryIngredients { get; set; } = new List<DeliveryIngredients>();

        // Navigation property: A delivery can have multiple DeliveryPackaging records.
        //public List<DeliveryPackaging> DeliveryPackaging { get; set; } = new List<DeliveryPackaging>();
    }
}

