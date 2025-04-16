using System;

namespace Mystefy.DTOs;

public class WasteLossRecordPackagingDTO
{
  // Tailor the manner in which the data should portray in the API response
    public int Id{ get; set; }
    public int QuantityLoss{ get; set; }
    public string? Reason{ get; set; }
    public DateTime DateOfLoss { get; set; } = DateTime.UtcNow;

    // Foreign key DTO
    public WasteLossRecordPackagingUserDTO? User { get; set; }
    public WasteLossRecordPackagingPackagingDTO? Ingredients { get; set; }
    public WasteLossRecordPackagingWarehouseDTO? Warehouse { get; set; }
}

public class WasteLossRecordPackagingUserDTO
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class WasteLossRecordPackagingPackagingDTO
{
    public int Id{ get; set; }
    public required string Name{ get; set; }
    public string? Type{ get; set; }
    public int Stock{ get; set; }
    public PackagingFinishedProductDTO? FinishedProduct { get; set; }
}

public class WasteLossRecordPackagingWarehouseDTO
{
    public int WarehouseID { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
}
