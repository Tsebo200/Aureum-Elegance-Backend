using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class FinishedProductPackaging
{
    [Key, Column(Order = 0)]
        public int ProductID { get; set; }

        [Key, Column(Order = 1)]
        public int PackagingID { get; set; }

        public decimal Amount { get; set; }

        // Navigation Properties
        public FinishedProduct? FinishedProduct { get; set; }
        public Packaging? Packaging { get; set; }

}
