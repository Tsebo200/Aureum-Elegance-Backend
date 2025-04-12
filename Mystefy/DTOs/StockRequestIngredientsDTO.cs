using System;

namespace Mystefy.DTOs;

public class StockRequestIngredientsDTO
{

 // Tailor the manner in which the data should portray in the API response
    public int Id { get; set; }
    public int AmountRequested { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }

    // Foreign key DTO
    public StockRequestIngredientsUserDTO? User { get; set; }
    public StockRequestIngredientsIngredientDTO? Ingredients { get; set; }
    public StockRequestIngredientsWarehouseDTO? Warehouse { get; set; }
}

public class StockRequestIngredientsUserDTO
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class StockRequestIngredientsIngredientDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Cost { get; set; } = string.Empty;
    public bool IsExpired { get; set; }

}

public class StockRequestIngredientsWarehouseDTO
{
    public int WarehouseID { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
}
