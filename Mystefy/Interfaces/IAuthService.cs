using System;
using Mystefy.Models;
using System.Threading.Tasks;

namespace Mystefy.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(User user);
        Task<string> HashPassword(string password);
        Task<User?> EmailExists(string email);
        Task<bool> LoginUser(string email, string password);
        Task<bool> ValidatePassword(string password);
    }
}
