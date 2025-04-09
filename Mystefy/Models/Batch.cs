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

        public int BatchSize {get; set;}

        public enum Status
        {
                Processing,
                Completed,
                Failed,
                Cancelled
        }
        
        public List<BatchFinishedProduct> BatchFinishedProducts {get; set;} = [];

}
