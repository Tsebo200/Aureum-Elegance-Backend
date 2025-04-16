using System;

namespace Mystefy.DTOs;

public class WasteLossRecordFragranceDTO
{
  // Tailor the manner in which the data should portray in the API response
    public int Id{ get; set; }
    public int QuantityLoss{ get; set; }
    public string? Reason{ get; set; }
    public DateTime DateOfLoss { get; set; } = DateTime.UtcNow;

    // Foreign key DTO
    public WasteLossRecordFragranceUserDTO? User { get; set; }
    public WasteLossRecordIngredientsFragranceDTO? Fragrance { get; set; }
    public WasteLossRecordFragranceWarehouseDTO? Warehouse { get; set; }
}
public class WasteLossRecordFragranceUserDTO
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class WasteLossRecordIngredientsFragranceDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Cost { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal Volume { get; set; }
}

public class WasteLossRecordFragranceWarehouseDTO
{
    public int WarehouseID { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
}
