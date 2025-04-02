using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Mystefy.Models;

public class WarehouseStock
{
    [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int StockID { get; set; }
  public decimal Stock { get; set; }
  
  
    // FK Constraint formed with Fragrance Table
    public int FragranceID{ get; set; }
    //Navigation Property of Fragrance
    public Fragrance? Fragrance{ get; set; }

 
    // FK Constraint formed with Warehouse Table
    public int WarehouseID{ get; set; }
    //Navigation Property of Warehouse
    public Warehouse? Warehouse{ get; set; }
}
