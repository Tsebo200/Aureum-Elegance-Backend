using System;

namespace Mystefy.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "Employee"; // Default role

        public UserDTO() {}

        public UserDTO(Models.User user)
        {
            UserId = user.UserId;
            Name = user.Name;
            Email = user.Email;
            Role = user.Role.ToString();
        }
    }
}

