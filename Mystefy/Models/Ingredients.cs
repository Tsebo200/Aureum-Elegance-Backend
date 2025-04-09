using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    // Ingredients entity - maps how the table in the database will look like.
    public class Ingredients
    {
        [Key] // Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] // Influences how the table will behave
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
        public string Cost { get; set; } = string.Empty;
        public bool IsExpired { get; set; }

        // Navigation Property
        public List<StockRequest> StockRequests { get; set; } = new List<StockRequest>();

        public List<FragranceIngredient>FragranceIngredients {get; set;} = [];
    }
}
