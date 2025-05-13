using System;

namespace Mystefy.DTOs
{
   public class FinishedProductDTO
{
    public int ProductID { get; set; }
    public int FragranceID { get; set; }
    // public int PackagingID { get; set; }
    public int Quantity { get; set; }

    public FinishedProductDTO() {}

    public FinishedProductDTO(Models.FinishedProduct model)
    {
        ProductID = model.ProductID;
        FragranceID = model.FragranceID;
        Quantity = model.Quantity;
    }
}
}

