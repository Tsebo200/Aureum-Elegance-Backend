using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class Warehouse
{
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WarehouseID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

       //Navigation Property
    public List<StockRequest>StockRequests {get; set;} = [];
}
