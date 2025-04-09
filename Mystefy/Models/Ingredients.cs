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
        // (Consider using a decimal type for numeric accuracy.)
        public string Cost { get; set; } = string.Empty;

        // Column to store the expiry date of the ingredient.
        public DateTime ExpiryDate { get; set; }

        // Boolean flag to indicate whether the ingredient has expired.
        public bool IsExpired { get; set; }

        // Navigation property: one ingredient can have multiple StockRequest records.
        public List<StockRequest> StockRequests { get; set; } = new List<StockRequest>();

        public List<FragranceIngredient>FragranceIngredients {get; set;} = [];
    }
}
