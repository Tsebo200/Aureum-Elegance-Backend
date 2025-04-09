using System;

namespace Mystefy.DTOs;

public class WarehouseStockDTO
{
    
    public int StockID {get; set;}
    public decimal Stock {get; set;}
    public  int WarehouseID {get; set;}
    public int FragranceID {get; set;}
    public WarehouseStockFragranceDTO? Fragrances {get; set;}
    public CheckWarehouseStockWarehouseDTO? Warehouses {get; set;}
}

public class WarehouseStockFragranceDTO
{
    public int Id {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public decimal Cost { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal Volume { get; set; }

}
public class CheckWarehouseStockWarehouseDTO
{
    public int? Id {get; set;}
    public string? Name {get; set;} = string.Empty;
    public string? Location {get; set;} = string.Empty;

}

public class CreateWarehouseStockDTO
{
    public decimal Stock {get; set;}
    public  int WarehouseID {get; set;}
    public int FragranceID {get; set;}
}

