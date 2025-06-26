using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtpNet;
using Mystefy.Data;
using Mystefy.Services;
using Mystefy.Models;
using Mystefy.DTOs;
namespace Mystefy.Controllers
{

    [ApiController]
    [Route("api/twofactor")]
    public class TwoFactorController : ControllerBase
    {
         private readonly MystefyDbContext _context;
        private readonly TwoFactorService _twoFactorService = new();

        public TwoFactorController(MystefyDbContext context)
        {
            _context = context;
        }

        [HttpGet("setup")]
        public IActionResult Setup2FA([FromQuery] string email)
        {
            var secret = _twoFactorService.GenerateSecret();
            var qrCodeUrl = _twoFactorService.GetQrCodeUri(email, secret);

            return Ok(new
            {
                secret,
                qrCodeUrl
            });
        }


        [HttpPost("verify")]
        public IActionResult VerifyLogin([FromBody] VerifyLoginDto dto)
        {
            if (dto.UserId == 0 || string.IsNullOrWhiteSpace(dto.Code))
                return BadRequest("UserId and code required");

            var user = _context.Users.Find(dto.UserId);
            if (user is null || user.TotpSecret == null)
                return Unauthorized("2FA not set up");

            bool ok = _twoFactorService.ValidateCode(user.TotpSecret, dto.Code);
            return Ok(new { valid = ok });
        }


    }
}
