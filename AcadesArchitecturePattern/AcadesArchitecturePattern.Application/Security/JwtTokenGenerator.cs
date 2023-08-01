using AcadesArchitecturePattern.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AcadesArchitecturePattern.Domain.Security
{
    public class JwtTokenGenerator
    {
        public string GenerateToken(AuthenticateResponse userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AcadesArchitecturePattern-authentication-key"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Name),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
            };

            var token = new JwtSecurityToken(
                "AcadesArchitecturePattern",
                "AcadesArchitecturePattern",
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
