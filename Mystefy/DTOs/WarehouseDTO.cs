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

    public List<wWarehouseStockDTO>? WarehouseStocks { get; set; }
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

public class WarehouseWasteLossRecordsForIngredientsDTO
{
    public string? Name {get; set;} = string.Empty;
    public string? location {get; set;} = string.Empty;
     public List<WarehouseWasteLossRecordIngredientsDTO> WasteLossRecordIngredients { get; set; } = [];


}

public class WarehouseWasteLossRecordsForFragrancesDTO
{
    public string? Name {get; set;} = string.Empty;
    public string? location {get; set;} = string.Empty;
     public List<WarehouseWasteLossRecordFragranceDTO> WasteLossRecordFragrance { get; set; } = [];

}

public class WarehouseWasteLossRecordsForBatchFinishedProductsDTO
{
    public string? Name {get; set;} = string.Empty;
    public string? location {get; set;} = string.Empty;
     public List<WarehouseWasteLossRecordBatchFinishedProductDTO> WasteLossRecordBatchFinishedProduct { get; set; } = [];

}
public class WarehouseWasteLossRecordsForPackagingDTO
{
    public string? Name {get; set;} = string.Empty;
    public string? location {get; set;} = string.Empty;
     public List<WarehouseWasteLossRecordPackagingDTO> WasteLossRecordPackaging { get; set; } = [];

}

public class WarehouseWasteLossRecordIngredientsDTO{

    public int Id{ get; set; }
    public int QuantityLoss{ get; set; }
    public string? Reason{ get; set; }
    public DateTime DateOfLoss { get; set; } 

    public WarehouseUserDTO? User { get; set; }
    public WarehouseWasteLossGetIngredientsDTO? Ingredients { get; set; }

}

public class WarehouseWasteLossRecordFragranceDTO{

    public int Id{ get; set; }
    public int QuantityLoss{ get; set; }
    public string? Reason{ get; set; }
    public DateTime DateOfLoss { get; set; } 

    public WarehouseUserDTO? User { get; set; }
    public WarehouseWasteLossGetFragranceDTO? Fragrances { get; set; }

}

public class WarehouseWasteLossRecordPackagingDTO{

    public int Id{ get; set; }
    public int QuantityLoss{ get; set; }
    public string? Reason{ get; set; }
    public DateTime DateOfLoss { get; set; } 

    public WarehouseUserDTO? User { get; set; }
    public WarehouseWasteLossGetPackagingDTO? Packaging { get; set; }

}

public class WarehouseWasteLossRecordBatchFinishedProductDTO{

    public int Id{ get; set; }
    public int QuantityLoss{ get; set; }
    public string? Reason{ get; set; }
    public DateTime DateOfLoss { get; set; } 

    public WarehouseUserDTO? User { get; set; }
    public WarehouseWasteLossGetFinishedProductDTO? FinishedProduct { get; set; }
     public WarehouseWasteLossRecordGetBatchDTO? Batch { get; set; }
}

public class WarehouseUserDTO
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
public class WarehouseWasteLossGetIngredientsDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Cost { get; set; } = string.Empty;
    public bool IsExpired { get; set; }

}

public class WarehouseWasteLossGetFragranceDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Cost { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal Volume { get; set; }
}

public class WarehouseWasteLossGetPackagingDTO
{
    public int Id{ get; set; }
    public required string Name{ get; set; }
    public string? Type{ get; set; }
    public int Stock{ get; set; }
}

public class WarehouseWasteLossGetFinishedProductDTO
{
    public int ProductID { get; set; }
    public int FragranceID { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
}

public class WarehouseWasteLossRecordGetBatchDTO
{
    public int BatchID { get; set; }
    public DateTime ProductionDate { get; set; }
    public int BatchSize {get; set;}

}