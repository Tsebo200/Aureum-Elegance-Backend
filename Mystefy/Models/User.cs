using System.ComponentModel.DataAnnotations;

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
    }

    
}
