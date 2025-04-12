//The BatchIngredientsDTO file defines the structure of data exchanged between the client and the server. 
// This ensures that only the relevant fields (BatchID, IngredientsID, and Quantity) are exposed.
using Mystefy.Models;

namespace Mystefy.DTOs
{
    public class BatchIngredientsDTO
    {
        public int BatchID { get; set; }
        public int IngredientsID { get; set; }
        public decimal Quantity { get; set; }
    }
    public static class BatchIngredientsMapper
    {
        public static BatchIngredientsDTO MapToDTO(BatchIngredients batchIngredient)
        {
            return new BatchIngredientsDTO
            {
                BatchID = batchIngredient.BatchID,
                IngredientsID = batchIngredient.IngredientsID,
                Quantity = batchIngredient.Quantity
            };
        }

        public static BatchIngredients MapToEntity(BatchIngredientsDTO batchIngredientDto)
        {
            return new BatchIngredients
            {
                BatchID = batchIngredientDto.BatchID,
                IngredientsID = batchIngredientDto.IngredientsID,
                Quantity = batchIngredientDto.Quantity
            };
        }
    }
    // This DTO class is used to transfer data between the client and server, ensuring that only the necessary fields are included in the API responses. The mapper methods convert between the DTO and the entity model, allowing for easy data manipulation and transfer.
}

