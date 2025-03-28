using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class Ingredients
{
    // Foreign Keys 
    [Key] // Attribute Annotation to behave as a primary key
    public int Id{ get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]//these influence how the table will behave
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Cost { get; set; } = string.Empty;
    public bool IsExpired { get; set; }

    //Navigation Property
    public List<StockRequest>StockRequests {get; set;} = [];
    
}

