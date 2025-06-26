using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;
using Mystefy.Services;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MystefyDbContext _context;
        private readonly IAuthService _authService;

        public UserController(MystefyDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // USER ENDPOINTS

        // GET: api/Mystefy/users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(user => new UserDTO(user)).ToList();
        }

        // POST: api/Mystefy/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password, // Will be hashed
                Role = Enum.TryParse<UserRole>(userDto.Role, out var role) ? role : UserRole.Employee
            };

            if (!await _authService.RegisterUser(user))
                return BadRequest("Email already exists");

            return Ok(new UserDTO(user));
        }

        // POST: api/Mystefy/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginDto)
        {
            // if (!await _authService.LoginUser(loginDto.Email, loginDto.Password))
            //     return Unauthorized("Invalid credentials");

            // var user = await _authService.EmailExists(loginDto.Email);
            // return Ok(new UserDTO(user!));

            if (!await _authService.LoginUser(loginDto.Email, loginDto.Password))
        return Unauthorized("Invalid credentials");

    var user = await _authService.EmailExists(loginDto.Email);
    if (user is null) return Unauthorized();

    // send the flag the frontend is waiting for
    return Ok(new UserDTO(user));
        }

        // DELETE: api/Mystefy/users
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/User/promote/{id}
        [HttpPut("promote/{id}")]
        public async Task<IActionResult> PromoteToManager(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Role = UserRole.Manager;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/User/remove/{id}
        [HttpPut("remove/{id}")]
        public async Task<IActionResult> DemoteToEmployee(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Role = UserRole.Employee;
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
