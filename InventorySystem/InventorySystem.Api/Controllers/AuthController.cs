using InventorySystem.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace InventorySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        // Update Constructor
        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // 1. Create the user object
            var user = new ApplicationUser 
            { 
                UserName = request.Email, 
                Email = request.Email,
                
                // Map the new fields
                ShopName = request.ShopName,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber, // Built-in field from IdentityUser
                PhysicalAddress = request.PhysicalAddress,
                AadhaarNumber = request.AadhaarNumber,
                BusinessCategory = request.BusinessCategory,
                Age = request.Age
            };

            // 2. Save to DB (This automatically hashes/encrypts the password)
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "Shopkeeper registered successfully!" });
            }

            // If it fails (e.g., password too weak), tell them why
            return BadRequest(result.Errors);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // 1. Check if user exists
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return Unauthorized(new { message = "Invalid email or password" });

            // 2. Check password
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid) return Unauthorized(new { message = "Invalid email or password" });

            // 3. Generate the Token (The Key Card)
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim("ShopName", user.ShopName ?? "Unknown Shop") // Store custom data inside the token!
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token lasts for 7 days
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }

    // Helper classes
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ShopName { get; set; } = string.Empty;
        
        // NEW FIELDS
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty; // We map this to IdentityUser.PhoneNumber
        public string PhysicalAddress { get; set; } = string.Empty;
        public string AadhaarNumber { get; set; } = string.Empty;
        public string BusinessCategory { get; set; } = string.Empty;
        public int Age { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
