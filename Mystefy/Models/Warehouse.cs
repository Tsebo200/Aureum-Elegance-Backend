using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class Warehouse
{
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WarehouseID { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Location { get; set; }

    // Assigned Manager reference
    public int? AssignedManagerUserId { get; set; }

    [ForeignKey("AssignedManagerUserId")]
    public User? AssignedManager { get; set; }

       //Navigation Property
   public List<StockRequest> StockRequests { get; set; } = [];

    public List<WarehouseStock> WarehouseStocks {get; set;} = [];
   public List<StockRequestIngredients> StockRequestIngredients { get; set; } = [];
   public List<StockRequestPackagings> StockRequestPackagings { get; set; } = [];
   public List<WasteLossRecordIngredients> WasteLossRecordIngredients { get; set; } = [];
   public List<WasteLossRecordPackaging> WasteLossRecordPackaging { get; set; } = [];
   public List<WasteLossRecordFragrance> WasteLossRecordFragrance { get; set; } = [];
   public List<WasteLossRecordBatchFinishedProducts> WasteLossRecordBatchFinishedProducts {get; set;} = [];
}
