using System;

namespace Mystefy.DTOs;

public class StockRequestDTO
{
    // Tailor the manner in which the data should portray in the API response
    public int Id { get; set; }
    public int AmountRequested { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }

    // Foreign key DTO
    public StockRequestUserDTO? User { get; set; }
    public StockRequestIngredientDTO? Ingredients { get; set; }
    public StockRequestWarehouseDTO? Warehouse { get; set; }
}

public class StockRequestUserDTO
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class StockRequestIngredientDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Cost { get; set; } = string.Empty;
    public bool IsExpired { get; set; }

}

public class StockRequestWarehouseDTO
{
    public int WarehouseID { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
}