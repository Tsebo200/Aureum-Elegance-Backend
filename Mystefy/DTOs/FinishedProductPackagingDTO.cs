using System;

namespace Mystefy.DTOs;


public class FinishedProductPackageDTO
{
    public int ProductID {get; set;}
    public int PackagingId {get; set;}
    public decimal Amount {get; set;}

     public IncludeFinishedProductInFinishedProductPackagingDTO? FinishedProduct {get; set;}
     public IncludePackagingInFinishedProductPackagingDTO? Packaging {get; set;}
}

public class PostFinishedProductPackagingDTO
{
     public int ProductID {get; set;}
    public int PackagingId {get; set;}
    public decimal Amount {get; set;}

    
}

public class IncludeFinishedProductInFinishedProductPackagingDTO
    {
        public int ProductID { get; set; }
        public int FragranceID { get; set; }

        public string? ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; } 

      
    }

public class IncludePackagingInFinishedProductPackagingDTO{

    public int Id{ get; set; }
    public required string Name{ get; set; }
    public string? Type{ get; set; }
    public int Stock{ get; set; }
}


