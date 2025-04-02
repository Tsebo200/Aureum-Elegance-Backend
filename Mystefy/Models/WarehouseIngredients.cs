using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    public class WarehouseIngredients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientId { get; set; }

        [Required]
        public string IngredientName { get; set; } = string.Empty;

        [Required]
        public int Volume { get; set; }
    }
}


