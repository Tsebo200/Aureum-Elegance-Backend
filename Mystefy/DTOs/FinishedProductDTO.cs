using System;

namespace Mystefy.DTOs
{
    public class FinishedProductDTO
    {

        public int ProductID { get; set; }
        public int FragranceID { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }

        public List<GetFinishedProductPackagingDTO>? FinishedProductPackaging { get; set; }
        public GetFragranceInFinishedProductDTO? Fragrance { get; set; }

    }
    public class PostFinishedProductDTO
    {

        public int ProductID { get; set; }
        public int FragranceID { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
    }
    public class GetFinishedProductPackagingDTO
    {
        public int ProductID { get; set; }
        public int PackagingId { get; set; }
        public decimal Amount { get; set; }

        public getPackagingInfoDTO? Packaging { get; set; }
    }

    public class getPackagingInfoDTO
    {

        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Type { get; set; }
        public int Stock { get; set; }
    
    }
}

public class GetFragranceInFinishedProductDTO
{
   public int Id {get;set;} 

   public string Name {get;set;}  = string.Empty;

   public string Description {get;set;}  = string.Empty;

   public decimal Volume  {get;set;} 

   public decimal Cost  {get;set;} 
   public DateTime ExpiryDate { get; set; }
    

}
