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
    }
}
