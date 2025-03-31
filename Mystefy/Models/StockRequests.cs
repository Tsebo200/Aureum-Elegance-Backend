using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Mystefy.Models;

public class StockRequest
{
    // Move enum outside the class to make it more accessible

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int AmountRequested { get; set; }

    [Required]
    // Change this to store as string in database
    [Column(TypeName = "text")]
    public string Status { get; set; } = "Pending";  // Changed from enum to string

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
