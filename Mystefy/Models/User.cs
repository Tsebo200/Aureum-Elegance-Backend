using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mystefy.Models
{
    public enum UserRole
    {
        Admin,
        Manager,
        Employee
    }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }
      
       //Navigation Property
    public List<StockRequest>StockRequests {get; set;} = [];

    public List<StockRequestIngredients> StockRequestIngredients { get; set; } = [];
    public List<StockRequestPackagings> StockRequestPackagings { get; set; } = [];
    public List<WasteLossRecordIngredients> WasteLossRecordIngredients { get; set; } = [];
    public List<WasteLossRecordPackaging> WasteLossRecordPackaging { get; set; } = [];
    public List<WasteLossRecordFragrance> WasteLossRecordFragrance { get; set; } = [];
    public List<WasteLossRecordBatchFinishedProducts> WasteLossRecordBatchFinishedProducts {get; set;} = [];
    }
}
