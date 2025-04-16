using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class Fragrance
{
     [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Cost { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal Volume { get; set; }

    public List<FinishedProduct>FinishedProduct {get; set;} = [];

    public List<WarehouseStock>WarehouseStocks {get; set;} = [];

    public List<FragranceIngredient>FragranceIngredients {get; set;} = [];
    public List<WasteLossRecordFragrance> WasteLossRecordFragrance { get; set; } = [];
}
