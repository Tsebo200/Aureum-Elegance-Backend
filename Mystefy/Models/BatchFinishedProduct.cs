using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    public class BatchFinishedProduct
    {
        [Key, Column(Order = 0)]
        public int BatchID { get; set; }

        [Key, Column(Order = 1)]
        public int ProductID { get; set; }

        public decimal Quantity { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;

        public int WarehouseID { get; set; }

        // Navigation (optional)
        public Batch? Batch { get; set; }
        public FinishedProduct? Product { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
