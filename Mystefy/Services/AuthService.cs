using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    public class AuthService : IAuthService
    {
        private readonly MystefyDbContext _context;

        public AuthService(MystefyDbContext context)
        {
            _context = context;
        }

        // Check if Email Exists
        public async Task<User?> EmailExists(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Hash Password using BCrypt
        public Task<string> HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
            return Task.FromResult(hashedPassword);
        }

        // Validate Password
        public bool ValidatePassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }

        // Validate Password with User's Hashed Password
        public Task<bool> ValidatePassword(User user, string password)
        {
             bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password);
             return Task.FromResult(isPasswordValid);
        }

        // Login User
        public async Task<bool> LoginUser(string email, string password)
        {
            var user = await EmailExists(email);
            if (user == null) return false;

            return BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password);
        }

        // Register User
        public async Task<bool> RegisterUser(User user)
        {
            var doesUserExist = await EmailExists(user.Email);
            if (doesUserExist != null)
            {
                return false; // User already exists
            }

            user.Password = await HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync(); // Ensure changes are saved before returning

            return true;
        }

        Task<bool> IAuthService.ValidatePassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}
