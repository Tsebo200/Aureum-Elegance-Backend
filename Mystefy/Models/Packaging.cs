using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class Packaging
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{ get; set; }
    [Required]
    [StringLength(100)]
    public required string Name{ get; set; }
    // public enum Type{ Glass_Bottles, Spray_Nozzles, Silk_Wraps }
    public string? Type{ get; set; }
    public int Stock{ get; set; }
}
