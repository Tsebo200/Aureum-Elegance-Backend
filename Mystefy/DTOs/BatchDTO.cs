using System;

namespace Mystefy.DTOs;

public class BatchDTO
{

    public DateTime ProductionDate { get; set; }

    public int BatchSize {get; set;}

    public string? Status {get; set;} = string.Empty;
}

public class BatchWithFinishedProductDTO
{
    public int BatchID { get; set; }

    public DateTime ProductionDate { get; set; }

    public int BatchSize {get; set;}

    public string? Status {get; set;} = string.Empty;

    public List<BatchFinishedProductInBatchDTO>? BatchFinishedProducts = [];
}

public class BatchFinishedProductInBatchDTO{
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int WarehouseID { get; set; }
}
