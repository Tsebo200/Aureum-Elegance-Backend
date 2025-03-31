using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MystefyController : ControllerBase
    {
        private readonly MystefyDbContext _context;
        private readonly IAuthService _authService;

        public MystefyController(MystefyDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // EXISTING PACKAGING ENDPOINTS

        // GET: api/Mystefy
        [HttpGet("Packaging")]
        public async Task<ActionResult<IEnumerable<Packaging>>> GetPackaging()
        {
            return await _context.Packaging.ToListAsync();
        }

        // GET: api/Mystefy/5
        [HttpGet("Packaging/{id}")]
        public async Task<ActionResult<Packaging>> GetPackaging(int id)
        {
            var packaging = await _context.Packaging.FindAsync(id);

            if (packaging == null)
            {
                return NotFound();
            }

            return packaging;
        }

        // PUT: api/Mystefy/5
        [HttpPut("Packaging/{id}")]
        public async Task<IActionResult> PutPackaging(int id, Packaging packaging)
        {
            if (id != packaging.Id)
            {
                return BadRequest();
            }

            _context.Entry(packaging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackagingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Mystefy
        [HttpPost]
        public async Task<ActionResult<Packaging>> PostPackaging(Packaging packaging)
        {
            _context.Packaging.Add(packaging);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackaging", new { id = packaging.Id }, packaging);
        }

        // DELETE: api/Mystefy/5
        [HttpDelete("Packaging/{id}")]
        public async Task<IActionResult> DeletePackaging(int id)
        {
            var packaging = await _context.Packaging.FindAsync(id);
            if (packaging == null)
            {
                return NotFound();
            }

            _context.Packaging.Remove(packaging);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackagingExists(int id)
        {
            return _context.Packaging.Any(e => e.Id == id);
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
            if (!await _authService.LoginUser(loginDto.Email, loginDto.Password))
                return Unauthorized("Invalid credentials");

            var user = await _authService.EmailExists(loginDto.Email);
            return Ok(new UserDTO(user!));
        }

        // DELETE: api/Mystefy/users/5
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // FINISHED PRODUCT ENDPOINTS

        // GET: api/Mystefy/finished-product
        [HttpGet("finished-product")]
        public async Task<ActionResult<IEnumerable<FinishedProduct>>> GetFinishedProduct()
        {
            return await _context.FinishedProduct.ToListAsync();
        }

        // GET: api/Mystefy/finished-product/5
        [HttpGet("finished-product/{id}")]
        public async Task<ActionResult<FinishedProduct>> GetFinishedProduct(int id)
        {
            var finishedProduct = await _context.FinishedProduct.FindAsync(id);
            if (finishedProduct == null)
            {
                return NotFound();
            }
            return finishedProduct;
        }

        // POST: api/Mystefy/finished-product
        [HttpPost("finished-product")]
        public async Task<ActionResult<FinishedProduct>> PostFinishedProduct(FinishedProduct finishedProduct)
        {
            _context.FinishedProduct.Add(finishedProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFinishedProduct), new { id = finishedProduct.ProductID }, finishedProduct);
        }

        // PUT: api/Mystefy/finished-product/5
        [HttpPut("finished-product/{id}")]
        public async Task<IActionResult> PutFinishedProduct(int id, FinishedProduct finishedProduct)
        {
            if (id != finishedProduct.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(finishedProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinishedProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Mystefy/finished-product/5
        [HttpDelete("finished-product/{id}")]
        public async Task<IActionResult> DeleteFinishedProduct(int id)
        {
            var finishedProduct = await _context.FinishedProduct.FindAsync(id);
            if (finishedProduct == null)
            {
                return NotFound();
            }

            _context.FinishedProduct.Remove(finishedProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper Method to Check If a Finished Product Exists
        private bool FinishedProductExists(int id)
        {
            return _context.FinishedProduct.Any(e => e.ProductID == id);
        }
    }
}
