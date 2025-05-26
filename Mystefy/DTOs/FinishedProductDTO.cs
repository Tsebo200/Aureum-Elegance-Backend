using System;

namespace Mystefy.DTOs
{
   public class FinishedProductDTO
{
    
    public int ProductID { get; set; }
    public int FragranceID { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }

    public FinishedProductDTO() {}

    public FinishedProductDTO(Models.FinishedProduct model)
    {
        ProductID = model.ProductID;
            ProductName = model.ProductName;
        FragranceID = model.FragranceID;
        Quantity = model.Quantity;
    }
}
}

