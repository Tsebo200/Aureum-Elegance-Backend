using System;

namespace Mystefy.DTOs;

public class StockRequestPackagingsDTO
{
 // Tailor the manner in which the data should portray in the API response
    public int Id { get; set; }
    public int AmountRequested { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }

    // Foreign key DTO
    public StockRequestPackagingsUserDTO? User { get; set; }
    public StockRequestPackagingsPackagingDTO? Packaging { get; set; }
    public StockRequestPackagingsWarehouseDTO? Warehouse { get; set; }
}

public class StockRequestPackagingsUserDTO
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class StockRequestPackagingsPackagingDTO{
    public int Id{ get; set; }
    public required string Name{ get; set; }
    public string? Type{ get; set; }
    public int Stock{ get; set; }
    // public PackagingFinishedProductDTO? FinishedProduct { get; set; }
}

public class StockRequestPackagingsWarehouseDTO
{
    public int WarehouseID { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
}

