using System;
using Mystefy.Models;

namespace Mystefy.DTOs
{
    public class DeliveryIngredientsDTO
    {
        // The foreign key referring to the Delivery.  
        public int DeliveryIngredientID { get; set; }

        // The foreign key referring to the Ingredient.
        public int IngredientID { get; set; }

        // The delivered quantity.
        public decimal QuantityDelivered { get; set; }

        // The date when the ingredient was ordered.
        public DateTime DateOrdered { get; set; }

        // The cost associated with the delivery of this ingredient.
        public decimal DeliveryCost { get; set; }
    }
    public static class DeliveryIngredientsMapper
    {
        public static DeliveryIngredientsDTO MapToDTO(DeliveryIngredients deliveryIngredients)
        {
            return new DeliveryIngredientsDTO
            {
                DeliveryIngredientID = deliveryIngredients.DeliveryIngredientID,
                IngredientID = deliveryIngredients.IngredientID,
                QuantityDelivered = deliveryIngredients.QuantityDelivered,
                DateOrdered = deliveryIngredients.DateOrdered,
                DeliveryCost = deliveryIngredients.DeliveryIngredientCost
            };
        }

        public static DeliveryIngredients MapToEntity(DeliveryIngredientsDTO deliveryIngredientsDto)
        {
            return new DeliveryIngredients
            {
                DeliveryIngredientID = deliveryIngredientsDto.DeliveryIngredientID,
                IngredientID = deliveryIngredientsDto.IngredientID,
                QuantityDelivered = deliveryIngredientsDto.QuantityDelivered,
                DateOrdered = deliveryIngredientsDto.DateOrdered,
                DeliveryIngredientCost = deliveryIngredientsDto.DeliveryCost
            };
        }
    }
}
