using System;

namespace Mystefy.DTOs
{
    public class BatchFinishedProductDTO
    {
        public int BatchID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int WarehouseID { get; set; }

        public BatchFinishedProductDTO() {}

    public BatchFinishedProductDTO(Models.BatchFinishedProduct model)
    {
        BatchID = model.BatchID;
        ProductID = model.ProductID;
        Quantity = model.Quantity;
        Unit = model.Unit;
        Status = model.Status;
        WarehouseID = model.WarehouseID;
    }
    }
}
