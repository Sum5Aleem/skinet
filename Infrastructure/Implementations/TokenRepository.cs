using Core.Entities;
using Core.Interfaces;
using Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly JwtSettings _JwtSettings;

        public TokenRepository(IOptions<JwtSettings> jwtSettings)
        {
            _JwtSettings = jwtSettings.Value;
        }
        public string GenerateJwtToken(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_JwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.DisplayName),
                }),
                Issuer = _JwtSettings.Issuer,
                Audience = _JwtSettings.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(_JwtSettings.TokenExpiryInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            //foreach (string role in userRoles)
            //{
            //    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            //}
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
