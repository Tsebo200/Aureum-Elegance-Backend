using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class WarehouseStock
{
    [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int StockID { get; set; }
  public decimal Stock { get; set; }
}
