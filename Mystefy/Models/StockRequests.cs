using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Mystefy.Models;

public class StockRequest

{
        // public enum StockStatus{ Pending, Approved , Rejected }
 
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{ get; set; }

    [Required]
    public int AmountRequested{ get; set; }
    [Required]
    public string Status { get; set; } = string.Empty;
    // public string? Status{ get; set; }
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;



    // Foreign Keys 
    // FK Constraint formed with Ingredients Table
    [Required]
    [ForeignKey("Ingredients")]
    public int IngredientsId { get; set; }
    //Navigation Property of Ingredients
    public virtual Ingredients? Ingredients { get; set; }

    [Required]
    [ForeignKey("User")]
    // FK Constraint formed with User Table
    public int? UserId { get; set; }
    //Navigation Property of Ingredients
    public virtual User? User { get; set; }

    // FK Constraint formed with Warehouse Table
    [Required]
    [ForeignKey("Warehouse")]
    public int WarehouseId { get; set; }
    //Navigation Property of Ingredients
    public virtual Warehouse? Warehouse { get; set; }

}
