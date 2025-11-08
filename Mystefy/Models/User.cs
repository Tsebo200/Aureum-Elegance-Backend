using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;   // for [JsonIgnore]

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
        [Required] public required string Name { get; set; }
        [Required, EmailAddress] public required string Email { get; set; }
        [Required] public required string Password { get; set; }
        [Required] public UserRole Role { get; set; }
        // ---------- 2-Factor fields ----------
        public bool Is2faEnabled { get; set; } = false;

        // Keep the secret server-side only
        [JsonIgnore]                 // never serialize to clients
        public string? TotpSecret { get; set; }

        // ---------- Navigation ----------
        public List<StockRequest> StockRequests { get; set; } = [];
        public List<StockRequestIngredients> StockRequestIngredients { get; set; } = [];
        public List<StockRequestPackagings> StockRequestPackagings { get; set; } = [];
        public List<WasteLossRecordIngredients> WasteLossRecordIngredients { get; set; } = [];
        public List<WasteLossRecordPackaging> WasteLossRecordPackaging { get; set; } = [];
        public List<WasteLossRecordFragrance> WasteLossRecordFragrance { get; set; } = [];
        public List<WasteLossRecordBatchFinishedProducts> WasteLossRecordBatchFinishedProducts { get; set; } = [];
    }
}
