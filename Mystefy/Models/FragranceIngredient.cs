using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class FragranceIngredient
{
     [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FragranceID { get; set; }
    public int IngredientID { get; set; }
    public decimal Amount { get; set; }
}
