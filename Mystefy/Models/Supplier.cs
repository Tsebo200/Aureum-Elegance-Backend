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
        public string SupplierName { get; set; } = string.Empty;

        [Required]
        public string ContactPerson { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        // 'string.Empty' is used to initialize the string properties to avoid null reference exceptions.

        // Navigation property: One-to-many relationship with Delivery
        public ICollection<Delivery> Delivery { get; set; } = new List<Delivery>();
    }
}

