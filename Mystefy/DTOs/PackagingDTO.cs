using System;
using System.Collections.Generic;

namespace Mystefy.DTOs;

public class PackagingDTO
{
    public int Id{ get; set; }
    public required string Name{ get; set; }
    public string? Type{ get; set; }
    public int Stock{ get; set; }
    

}

public class PackagingFinishedProductDTO
{
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int FragranceID { get; set; }
        public int Quantity { get; set; }
}