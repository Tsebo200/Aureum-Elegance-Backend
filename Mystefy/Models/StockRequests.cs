using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class StockRequest

{
    // public enum StockStatus{ InStock, OutOfStock ,LimitedStock}

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{ get; set; }

    [Required]
    public int AmountRequested{ get; set; }
    [Required]
    public string? Status{ get; set; }
    // public string? Status{ get; set; }
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;



    // Foreign Keys 
    [Required]
    [ForeignKey("Ingredients")]
    public int IngredientsId { get; set; }

    //Navigation Property
    public Ingredients Ingredients{ get; set; } = null!;


    //     [Required]
    // public string? ApprovedBy{ get; set; }
}
