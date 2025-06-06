using Mystefy.Models;

namespace Mystefy.DTOs;

public class BatchFinishedProductDTO
{
    public int BatchID { get; set; }
    public int ProductID { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int WarehouseID { get; set; }

    public BatchDTO? Batch { get; set; }
    public FinishedProductDTO? FinishedProduct { get; set; }

    public BatchFinishedProductDTO() {}

    public BatchFinishedProductDTO(BatchFinishedProduct model)
    {
        BatchID = model.BatchID;
        ProductID = model.ProductID;
        Quantity = model.Quantity;
        Unit = model.Unit;
        Status = model.Status.ToString();
        WarehouseID = model.WarehouseID;

        // Manual mapping (safe even if navigation property is not loaded)
        if (model.Batch != null)
        {
            Batch = new BatchDTO
            {
                ProductionDate = model.Batch.ProductionDate,
                BatchSize = model.Batch.BatchSize,
                Status = model.Batch.Status.ToString()
            };
        }

        if (model.FinishedProduct != null)
        {
            FinishedProduct = new FinishedProductDTO
            {
                ProductID = model.FinishedProduct.ProductID,
                ProductName = model.FinishedProduct.ProductName,
                FragranceID = model.FinishedProduct.FragranceID,
                Quantity = model.FinishedProduct.Quantity,
                // Optional: Set Fragrance and FinishedProductPackaging if already included in the query
                Fragrance = null,
                FinishedProductPackaging = null
            };
        }
    }
}
