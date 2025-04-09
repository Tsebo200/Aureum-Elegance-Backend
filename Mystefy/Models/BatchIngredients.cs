//The BatchIngredients model represents the table structure in the database and defines relationships 
// (via foreign keys) with the Batch and Ingredients entities.
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    // Represents the join entity between Batch and Ingredients
    public class BatchIngredients
    {
        // Foreign Key to Batch
        [Required]
        public int BatchID { get; set; }

        // Foreign Key to Ingredients
        [Required]
        public int IngredientsID { get; set; }

        // Quantity for this ingredient in the batch
        [Required]
        public decimal Quantity { get; set; }

        // Navigation properties (optional, if you have Batch and Ingredients models)
        public Batch? Batch { get; set; }
        public Ingredients? Ingredients { get; set; }
    }
}
// This class represents a many-to-many relationship between Batch and Ingredients, where each batch can have multiple ingredients and each ingredient can be part of multiple batches. The Quantity property indicates how much of that ingredient is used in the batch.
// The BatchID and IngredientsID properties are foreign keys that link to the respective Batch and Ingredients entities. The navigation properties allow for easy access to the related entities.