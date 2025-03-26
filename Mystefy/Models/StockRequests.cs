using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
    // FK Constraint formed with Ingredients Table
    [AllowNull]
    [ForeignKey("Ingredients")]
    public int IngredientsId { get; set; }
    //Navigation Property of Ingredients
    public Ingredients Ingredients{ get; set; } = null!;

    [AllowNull]
    [ForeignKey("User")]
    // FK Constraint formed with User Table
    public int? UserId{ get; set; }
    //Navigation Property of Ingredients
    public User User{ get; set; } = null!;

    // FK Constraint formed with Warehouse Table
    [AllowNull]
    [ForeignKey("Warehouse")]
    public int WarehouseId { get; set; }
    //Navigation Property of Ingredients
    public Warehouse Warehouse{ get; set; } = null!;

}
