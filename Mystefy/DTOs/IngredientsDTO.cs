// This DTO is used to transfer data related to ingredients between the application and the client.
// It contains properties that represent the data fields of the Ingredients entity.
using System;
using System.Collections.Generic;

namespace Mystefy.DTOs
{
    public class IngredientsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Cost { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public bool IsExpired { get; set; }

        // Optional: Include associated BatchIngredients details if needed.
        public List<BatchIngredientsDTO>? BatchIngredients { get; set; }
    }
}



