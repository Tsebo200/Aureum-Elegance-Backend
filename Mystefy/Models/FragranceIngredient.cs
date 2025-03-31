using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Mystefy.Models;

public class FragranceIngredient
{
     [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    
    public decimal Amount { get; set; }

   
    // public int FragranceId { get; set; }
    // public Fragrance? Fragrance {get; set;}
    // public int IngredientsId {get; set;}
    // public Ingredients? Ingredients {get; set;}
}
