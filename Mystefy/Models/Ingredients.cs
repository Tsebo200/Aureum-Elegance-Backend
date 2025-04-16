using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    // Represents the Ingredients entity, mapping to a database table.
    public class Ingredients
    {
        [Key] // Marks this property as the primary key of the table.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Database auto-generates the ID.
        public int Id { get; set; }

        [Required] // This field must be provided.
        [StringLength(100)] // Limits the length of the Name column to 100 characters.
        public string Name { get; set; } = string.Empty;

        // Represents the type of ingredient (e.g., vegetable, spice, dairy, etc.)
        public string Type { get; set; } = string.Empty;

        // Represents the cost of the ingredient.
        // (Consider using a decimal type for numeric accuracy, but here it is left as a string.)
        public string Cost { get; set; } = string.Empty;

        // Column to store the expiry date of the ingredient.
        public DateTime ExpiryDate { get; set; }

        // Boolean flag to indicate whether the ingredient has expired.
        public bool IsExpired { get; set; }

        // Navigation property: one ingredient can have multiple stock request records.
        public List<StockRequest> StockRequests { get; set; } = new List<StockRequest>();

        // Navigation property: one ingredient can be part of multiple delivery records.
        public List<DeliveryIngredients> DeliveryIngredients { get; set; } = new List<DeliveryIngredients>();

        // Navigation property: one ingredient can be linked with multiple fragrance records.
        public List<FragranceIngredient> FragranceIngredients { get; set; } = new List<FragranceIngredient>();

        // Navigation property: one ingredient can have multiple stock request ingredients.
        public List<StockRequestIngredients> StockRequestIngredients { get; set; } = new List<StockRequestIngredients>();

        // Navigation property: one ingredient can have multiple stock request packagings.
        public List<StockRequestPackagings> StockRequestPackagings { get; set; } = new List<StockRequestPackagings>();

        // Navigation property: one ingredient can be associated with multiple warehouse ingredient records.
        public List<WarehouseIngredients> WarehouseIngredients { get; set; } = new List<WarehouseIngredients>();

        // Navigation property: one ingredient can be associated with multiple WasteLossRecordIngredients ingredient records.
        public List<WasteLossRecordIngredients> WasteLossRecordIngredients { get; set; } = new List<WasteLossRecordIngredients>();
    }
}


