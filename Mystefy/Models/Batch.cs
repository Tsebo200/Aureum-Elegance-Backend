using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public class Batch
{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchID { get; set; }

        public DateTime ProductionDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        // Navigation Properties
        public List<FinishedProduct> FinishedProducts { get; set; } = [];
    

}
