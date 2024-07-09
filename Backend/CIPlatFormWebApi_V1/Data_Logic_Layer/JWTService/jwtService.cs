﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Data_Access_Layer.JWTService
{
    public class JwtService
    {
        private readonly string _secretKey;
        private readonly int _tokenDuration;

        public JwtService(IConfiguration configuration)
        {
            _secretKey = configuration.GetValue<string>("JwtConfig:Key");
            _tokenDuration = configuration.GetValue<int>("JwtConfig:Duration");
        }

        public string GenerateToken(string userId, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("userId", userId),
                new Claim(ClaimTypes.Role, role),
            };

            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddHours(_tokenDuration),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
