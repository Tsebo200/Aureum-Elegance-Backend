using System;

namespace Mystefy.DTOs
{
    public class SupplierDTO
    {
        public int SupplierID { get; set; }
        public required string? SupplierName { get; set; }
        public required string? ContactPerson { get; set; }
        public required string? PhoneNumber { get; set; }
    }
}
