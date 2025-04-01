using System;

namespace Mystefy.DTOs;

public class StockRequestDTO
{
    public int Id{ get; set; }
    public int AmountRequested{ get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    public StockRequestUserDTO? User { get; set; }

    // Foreign key
    // public int IngredientsId { get; set; }
    // public int? UserId { get; set; }
    // public int WarehouseId { get; set; }
}

public class StockRequestUserDTO
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = "Employee"; // Default role
}
