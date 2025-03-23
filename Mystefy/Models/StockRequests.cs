using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class StockRequest

{
    public enum StockStatus{ InStock, OutOfStock ,LimitedStock}

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RequestId{ get; set; }

    [Required]
    public int AmountRequested{ get; set; }
    [Required]
    public StockStatus Status{ get; set; }
    // public string? Status{ get; set; }
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;

    // Foreign Keys 
    // public int IngredientId{ get; set; }

    //     [Required]
    // public string? ApprovedBy{ get; set; }
}
