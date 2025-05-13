using System;

namespace Mystefy.DTOs;

public class WasteLossRecordBatchFinishedProductsDTO
{
    public int Id{ get; set; }
    public int QuantityLoss{ get; set; }
    public string? Reason{ get; set; }
    public DateTime DateOfLoss { get; set; } = DateTime.UtcNow;

    // Foreign key DTO
    public WasteLossRecordBatchFinishedProductsUserDTO? User { get; set; }
    public WasteLossRecordBatchFinishedProductsFinishedProductDTO? FinishedProduct { get; set; }
    public WasteLossRecordBatchFinishedProductsBatchDTO? Batch { get; set; }
    public WasteLossRecordBatchFinishedProductsWarehouseDTO? Warehouse { get; set; }
}
public class WasteLossRecordBatchFinishedProductsUserDTO
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class WasteLossRecordBatchFinishedProductsFinishedProductDTO
{
    public int ProductID { get; set; }
    public int FragranceID { get; set; }
    // public int PackagingID { get; set; }
    public int Quantity { get; set; }
}

public class WasteLossRecordBatchFinishedProductsBatchDTO
{
    public int BatchID { get; set; }
    public DateTime ProductionDate { get; set; }
    public int BatchSize {get; set;}
    // may have to tweak this later as BatchDTO is not done yet
}

public class WasteLossRecordBatchFinishedProductsWarehouseDTO
{
    public int WarehouseID { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
}


