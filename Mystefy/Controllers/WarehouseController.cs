using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mystefy.Data;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
       private readonly MystefyDbContext _context;

        public WarehouseController(MystefyDbContext context)
        {
            _context = context;
        }
 
    }
}
