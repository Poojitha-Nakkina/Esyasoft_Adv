using AMI_ProjectAPI.Models;
using AMI_ProjectAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using BCrypt.Net;
namespace AMI_ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AmiContext _context;
        private readonly IConfiguration _config;

        public AuthController(AmiContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ✅ REGISTER (Consumers register freely)
        [AllowAnonymous]
        [HttpPost("register-consumer")]
        public async Task<IActionResult> RegisterConsumer([FromBody] User newUser)
        {
            if (newUser == null || string.IsNullOrEmpty(newUser.UserName) ||
                string.IsNullOrEmpty(newUser.Password) || string.IsNullOrEmpty(newUser.Email))
                return BadRequest("Username, Password, and Email are required");

            if (await _context.Users.AnyAsync(u => u.Email == newUser.Email))
                return Conflict("Email already exists");

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Role = "Consumer";
            newUser.Active = true;
            // 🎯 Step 2: Check for existing consumer by email
            var existingConsumer = await _context.Consumers.FirstOrDefaultAsync(c => c.Email == newUser.Email);

            if (existingConsumer != null)
            {
                // 🎯 Step 3: Auto-link existing consumer details
                newUser.ConsumerId = existingConsumer.ConsumerId;

                // Optional: Sync display data from Consumer to User
                newUser.UserName = existingConsumer.Name ?? newUser.UserName;
                newUser.Phone = existingConsumer.Phone ?? newUser.Phone;
                newUser.DisplayName = existingConsumer.Name ?? newUser.DisplayName;

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Existing consumer linked successfully.",
                    userId = newUser.UserId,
                    consumerId = existingConsumer.ConsumerId
                });
            }

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Consumer registered successfully" });
        }

        // ✅ REGISTER (Only SuperAdmin can create ZoneAdmins)
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("register-zoneadmin")]
        public async Task<IActionResult> RegisterZoneAdmin([FromBody] User newUser)
        {
            if (newUser == null || string.IsNullOrEmpty(newUser.UserName) ||
                string.IsNullOrEmpty(newUser.Password) || string.IsNullOrEmpty(newUser.Email))
                return BadRequest("Username, Password, and Email are required");

            if (await _context.Users.AnyAsync(u => u.Email == newUser.Email))
                return Conflict("Email already exists");

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Role = "ZoneAdmin";

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "ZoneAdmin created successfully" });
        }

        // ✅ LOGIN (All users)
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            // ✅ Step 1: Check user existence
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
                return Unauthorized("User not found");

            // ✅ Step 2: Verify password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
            if (!isPasswordValid)
                return Unauthorized("Invalid password");

            // ✅ Step 3: Update last login
            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // ✅ Step 4: Find consumerId for this user (only if it's a consumer)
            int? consumerId = null;
            if (user.Role == "Consumer")
            {
                var consumer = await _context.Consumers
                    .FirstOrDefaultAsync(c => c.Email == user.Email); // assuming Consumers table has Email
                consumerId = consumer?.ConsumerId;

            }


            // ✅ Step 5: Generate JWT
            var token = GenerateJwtToken(user);

            // ✅ Step 6: Send complete response
            return Ok(new
            {
                token,
                role = user.Role,
                email = user.Email,
                userId = user.UserId,
                displayName = user.DisplayName,
                consumerId = consumerId, // ✅ include it if available
                lastLogin = user.LastLogin
            });
        }


        // ✅ FORGOT PASSWORD (Send Reset Token via Email)
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return NotFound("Email not found");

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            user.ResetToken = token;
            user.TokenExpiry = DateTime.UtcNow.AddMinutes(15);

            await _context.SaveChangesAsync();
            SendResetEmail(email, token);

            return Ok("Password reset link sent to your email.");
        }

        // ✅ RESET PASSWORD
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ResetToken == model.Token && u.TokenExpiry > DateTime.UtcNow);
            if (user == null)
                return BadRequest("Invalid or expired token");

            user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            user.ResetToken = null;
            user.TokenExpiry = null;

            await _context.SaveChangesAsync();
            return Ok("Password reset successfully");
        }

        // ✅ Helper: Generate JWT Token with Role Claim
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ✅ Helper: Send Email
        private void SendResetEmail(string email, string token)
        {
            var resetLink = $"https://localhost:7071/Auth/ResetPassword?token={token}";
            var smtpSettings = _config.GetSection("Smtp");

            var mail = new MailMessage(smtpSettings["Email"], email)
            {
                Subject = "Password Reset",
                Body = $"Click the link to reset your password:\n{resetLink}",
                IsBodyHtml = false
            };

            var smtp = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"]))
            {
                Credentials = new System.Net.NetworkCredential(smtpSettings["Email"], smtpSettings["AppPassword"]),
                EnableSsl = true
            };

            smtp.Send(mail);
        }
    }

    // ✅ DTOs
    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class ResetPasswordRequest
    {
        public string Token { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }
}