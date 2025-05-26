using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models;

public enum BatchStatus
        {
                Processing,
                Completed,
                Failed,
                Cancelled
        }
public class Batch
{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchID { get; set; }

        public DateTime ProductionDate { get; set; }

        public int BatchSize {get; set;}

        public BatchStatus Status {get; set;}
        
        public List<BatchFinishedProduct> BatchFinishedProducts {get; set;} = [];
        public List<WasteLossRecordBatchFinishedProducts> WasteLossRecordBatchFinishedProducts {get; set;} = [];

}
