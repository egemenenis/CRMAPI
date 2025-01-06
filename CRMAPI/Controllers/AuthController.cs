using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/auth
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        // GET: api/auth/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        [Authorize(Roles = "User")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == login.Username);
            if (user == null || !VerifyPasswordHash(login.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        // POST: api/auth/register
        [HttpPost("register")]
        [Authorize(Roles = "User")]
        public IActionResult Register([FromBody] RegisterModel register)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Username == register.Username);
            if (existingUser != null)
                return BadRequest("Username is already taken.");

            var passwordHash = CreatePasswordHash(register.Password);

            var user = new UserModel
            {
                Username = register.Username,
                PasswordHash = passwordHash,
                Role = register.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Login), new { username = user.Username }, user);
        }

        // PUT: api/auth/update/{id}
        [HttpPut("update/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult UpdateUser(int id, [FromBody] RegisterModel updatedUser)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            existingUser.Username = updatedUser.Username;
            existingUser.Role = updatedUser.Role;

            if (!string.IsNullOrEmpty(updatedUser.Password))
            {
                existingUser.PasswordHash = CreatePasswordHash(updatedUser.Password);
            }

            _context.Users.Update(existingUser);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/auth/delete/{id}
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return storedHash == storedHash;
        }

        private string CreatePasswordHash(string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return Convert.ToBase64String(salt) + ":" + hash;
        }

        private string GenerateJwtToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}