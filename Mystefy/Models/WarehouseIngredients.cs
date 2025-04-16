using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    // Represents the WarehouseIngredients entity, mapping to a database table
    public class WarehouseIngredients
    {
        [Key] // Marks this property as the primary key of the table
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensures the database auto-generates the ID
        public int IngredientId { get; set; }

        [Required] // Ensures this field must be provided (not nullable)
        public string IngredientName { get; set; } = string.Empty;

        [Required] // Ensures this field must be provided (not nullable)
        public int Volume { get; set; } // Represents the quantity of the ingredient in stock
    }
}

