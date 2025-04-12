using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    public class DeliveryIngredients
    {
        // Composite Key: DeliveryIngredientID and IngredientID together form the primary key.
        // DeliveryIngredientID is a foreign key to Delivery.
        [ForeignKey(nameof(Delivery))]
        public int DeliveryIngredientID { get; set; }

        // IngredientID is a foreign key to the Ingredients entity.
        [ForeignKey(nameof(Ingredient))]
        public int IngredientID { get; set; }

        // The quantity of the ingredient delivered.
        public decimal QuantityDelivered { get; set; }

        // The date when the ingredient was ordered.
        public DateTime DateOrdered { get; set; }

        // The cost of the delivery.
        public decimal DeliveryIngredientCost { get; set; }

        // Navigation property: the Delivery associated with this record.
        public Delivery Delivery { get; set; } = null!;

        // Navigation property: the Ingredient associated with this record.
        public Ingredients Ingredient { get; set; } = null!;
    }
}
