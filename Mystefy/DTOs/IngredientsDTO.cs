using System;

namespace Mystefy.DTOs
{
    public class IngredientsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Cost { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsExpired { get; set; }
    }
}

