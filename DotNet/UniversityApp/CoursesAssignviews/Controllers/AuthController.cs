using CoursesAssignviews.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;


namespace CoursesAssignviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IConfiguration _configuration;
        //private readonly CollegeDbContext _context;

        //public AuthController(IConfiguration configuration, CollegeDbContext context)
        //{
        //    _configuration = configuration;
        //    _context = context;
        //}

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginModel user)
        //{
        //    // Dummy hardcoded user for quick testing
        //    if (user.Username == "admin" && user.Password == "admin123")
        //    {
        //        var token = GenerateJwtToken(new User
        //        {
        //            UserName = "admin",
        //            Role = "Admin"
        //        });
        //        return Ok(new { token });
        //    }

        //    return Unauthorized("Invalid credentials");
        //}

        //private string GenerateJwtToken(User user)
        //{
        //    var jwtSettings = _configuration.GetSection("Jwt");
        //    var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Name, user.UserName),
        //        new Claim(ClaimTypes.Role, user.Role)
        //    };

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
        //        Issuer = jwtSettings["Issuer"],
        //        Audience = jwtSettings["Audience"],
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}


        //public class LoginModel
        //{
        //    public string Username { get; set; }
        //    public string Password { get; set; }
        //}


        private readonly IConfiguration _configuration;
        private readonly CollegeDbContext _context;
        public AuthController(IConfiguration configuration, CollegeDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            // Query the database for the user
            var dbUser = _context.Users
       .FirstOrDefault(u => u.Username == user.Username);
            if (dbUser != null && BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {
                // Successful authentication: Generate JWT token
                var token = GenerateJwtToken(dbUser);  // Pass the dbUser directly
                return Ok(new { token });
            }
            return Unauthorized("Invalid credentials");
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel user)
        {
            //if (string.IsNullOrWhiteSpace(user.Username) ||
            //    string.IsNullOrWhiteSpace(user.Password) ||
            //    string.IsNullOrWhiteSpace(user.Email))
            //{
            //    return BadRequest("Username, password and email are required.");
            //}


            // Check if username already exists
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest("Username already exists.");
            }

            if (_context.Users.Any(u => u.Email == user.Email))
                return BadRequest("Email already registered.");
            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            // Create new user
            var newUser = new UserLogin
            {
                Username = user.Username,
                Password = hashedPassword,
                Role = user.Role ?? "User",
                Email = user.Email// Default to "User" if no role provided
            };
            // Save to database
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok("User registered successfully.");
        }

        private object GenerateJwtToken(UserLogin user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
            public class RegisterModel
            {
                public string Username { get; set; }
                public string Password { get; set; }
                public string Role { get; set; } 
            // Optional; defaults to "User"
                 public string Email { get; set; }
            }


        }
    }
