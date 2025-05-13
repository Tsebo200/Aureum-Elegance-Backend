using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    public class FinishedProduct
    {
        [Key]
        public int ProductID { get; set; }

        [ForeignKey("Fragrance")]
        public int FragranceID { get; set; }
        public Fragrance Fragrance { get; set; } = null!;

        

        [Required]
        public int Quantity { get; set; }

        // Navigation Property 
        public List<WasteLossRecordBatchFinishedProducts> WasteLossRecordBatchFinishedProducts {get; set;} = [];
    }
  
}
