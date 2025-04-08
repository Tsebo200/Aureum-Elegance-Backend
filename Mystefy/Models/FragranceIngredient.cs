using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Mystefy.Models;

public class FragranceIngredient
{
      [Key, Column(Order = 0)]
        public int FragranceID { get; set; }

        [Key, Column(Order = 1)]
        public int IngredientsID { get; set; }

        public decimal Amount { get; set; }

        // Navigation Properties
        public Fragrance? Fragrance { get; set; }
        public Ingredients? Ingredients { get; set; }
}
