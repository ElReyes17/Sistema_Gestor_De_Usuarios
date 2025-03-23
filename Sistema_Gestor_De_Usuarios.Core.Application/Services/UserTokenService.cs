
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Services;
using Sistema_Gestor_De_Usuarios.Core.Domain.Entities;
using Sistema_Gestor_De_Usuarios.Core.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly JWTSetting _jwtSettings;
        public UserTokenService(IOptions<JWTSetting> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public Task<string> BuildTokenAsync(User user)
        {
            var claims = new List<Claim>
        {
            new (ClaimTypes.Email, user.Email)
        };

            // Clave
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var symmetricSecurityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            // Crear el token JWT
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
        }
    }
}
