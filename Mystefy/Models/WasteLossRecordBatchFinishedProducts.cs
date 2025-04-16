using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class WasteLossRecordBatchFinishedProducts
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{ get; set; }
    [Required]
    public int QuantityLoss{ get; set; }
    [Required]
    public string? Reason{ get; set; }
    public DateTime DateOfLoss { get; set; } = DateTime.UtcNow;

    // Foreign Keys 
    // FK Constraint formed with User Table
    [Required]
    [ForeignKey("User")]
    public int? UserId { get; set; }
    //Navigation Property of User
    public User? User { get; set; }


    // FK Constraint formed with FinishedProduct Table
    [Required]
    [ForeignKey("FinishedProduct")]
    public int ProductId { get; set; }
    //Navigation Property of FinishedProduct
    public FinishedProduct? FinishedProduct { get; set; }
    

    // FK Constraint formed with Batch Table
    [Required]
    [ForeignKey("Batch")]
    public int BatchId { get; set; }
    //Navigation Property of Batch
    public Batch? Batch { get; set; }


    // FK Constraint formed with Warehouse Table
    [Required]
    [ForeignKey("Warehouse")]
    public int WarehouseId { get; set; }
    //Navigation Property of Warehouse
    public Warehouse? Warehouse { get; set; }

}
