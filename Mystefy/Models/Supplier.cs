using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }

        [Required]
        public string SupplierName { get; set; }

        [Required]
        public string ContactPerson { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        // Navigation property: One-to-many relationship with Delivery
        public ICollection<Delivery> Delivery { get; set; }
    }
}

