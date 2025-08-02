using ApiProductos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiProductos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly JwtSettings _jwt;

        public AuthController(IOptions<JwtSettings> options)
        {
            _jwt = options.Value;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login dto)
        {
            // 1. Validar contra settings
            if (dto.User != _jwt.Username || dto.Password != _jwt.Password)
                return Unauthorized(new { message = "Credenciales inválidas" });

            // 2. Crear claims, firmar token… (igual que antes)

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, dto.User),
            new Claim("role", "Admin")
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiresInMinutes),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}
