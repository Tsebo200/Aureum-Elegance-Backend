using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;
// public enum Type{ Glass_Bottles, Spray_Nozzles, Silk_Wraps }
public class Packaging
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    public string? Type { get; set; }
    public int Stock { get; set; }

    // Navigation property

    public List<StockRequestPackagings> StockRequestPackagings { get; set; } = [];
    public List<WasteLossRecordPackaging> WasteLossRecordPackaging { get; set; } = [];
    public List<FinishedProductPackaging> FinishedProductPackaging {get; set;} = [];

}
