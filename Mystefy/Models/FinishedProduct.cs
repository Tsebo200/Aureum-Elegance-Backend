using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    public class FinishedProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [Required]
        public string? ProductName { get; set; }
        [ForeignKey("Fragrance")]
        public int FragranceID { get; set; }
        public Fragrance Fragrance { get; set; } = null!;



        [Required]
        public int Quantity { get; set; }

        // Navigation Property 
        public List<WasteLossRecordBatchFinishedProducts> WasteLossRecordBatchFinishedProducts { get; set; } = [];
        public List<FinishedProductPackaging> FinishedProductPackaging { get; set; } = [];
        public List<BatchFinishedProduct> BatchFinishedProducts { get; set; } = [];

    }
  
}
