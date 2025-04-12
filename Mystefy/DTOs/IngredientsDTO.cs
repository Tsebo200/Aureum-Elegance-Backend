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

        // Optional: Include related delivery details.
        public List<DeliveryIngredientsDTO>? DeliveryIngredients { get; set; }

        // Optional: Include related fragrance details.
        //public List<FragranceIngredientsDTO>? FragranceIngredients { get; set; }

        // Optional: Include related stock request details.
        public List<StockRequestDTO>? StockRequests { get; set; }

        // Optional: Include related waste/loss records.
        //public List<WasteLossRecordDTO>? WasteLossRecords { get; set; }

        // Optional: Include related warehouse ingredient details.
        //public List<WarehouseIngredientsDTO>? WarehouseIngredients { get; set; }
    }
}




