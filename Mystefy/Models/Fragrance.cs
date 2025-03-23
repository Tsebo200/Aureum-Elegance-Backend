using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class Fragrance
{
     [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public int FragranceID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal Volume { get; set; }
}
