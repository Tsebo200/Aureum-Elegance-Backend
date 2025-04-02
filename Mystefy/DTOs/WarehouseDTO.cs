using System;

namespace Mystefy.DTOs;

public class WarehouseDTO
{
    public string? Name {get; set;} = string.Empty;
    public string? location {get; set;} = string.Empty;

}

public class WarehouseStockRequestDTO
{
    public string? Name {get; set;} = string.Empty;
    public string? location {get; set;} = string.Empty;

    public WStockRequestsDTO? StockRequests {get; set;}

}
public class WarehouseShowStock{
    public string? Name {get; set;} = string.Empty;
    public string? location {get; set;} = string.Empty;

    public wWarehouseStockDTO? WarehouseStocks {get; set;}
}

public class WStockRequestsDTO
{
    public int Id { get; set; }
    public int AmountRequested { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }
}

public class wWarehouseStockDTO
{
    public int StockID {get; set;}
    
    public decimal Stock {get; set;}
    public  int WarehouseID {get; set;}
    public int FragranceID {get; set;}
    public WarehouseStockAddFragranceDTO? Fragrances {get; set;}
}

public class WarehouseStockAddFragranceDTO
{
    public int Id {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public decimal Cost { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal Volume { get; set; }

}