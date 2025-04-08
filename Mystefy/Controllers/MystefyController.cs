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

     
    }
}
