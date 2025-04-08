using BetThanYes.Domain.DTOs.Response.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Infrastructure.Services.Auth;
using BetThanYes.Domain.DTOs.Response.Login;
using BetThanYes.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using BetThanYes.Domain.DTOs.Request.Auth;

namespace BetThanYes.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _AuthRepository;
        private readonly IConfiguration _config;
        public AuthService(IAuthRepository AuthRepository, IConfiguration configuration)
        {
            _AuthRepository = AuthRepository;
            _config = configuration;
        }

        public async Task<ValidateEmailResponse> ValidateEmail(string email)
        {
            var user = await _AuthRepository.GetByEmail(email);
            var response = new ValidateEmailResponse();
            if (user != null)
            {
                response.Result = true;
                return response;
            }
            response.Result = false;
            return response;
        }

        public async Task<LoginResponse> GetNewToken(Guid id, string email, int tokenType)
        {
            //tokenType 1 = short, tokenType 2 = long
            LoginResponse response = new LoginResponse();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: tokenType == 1 ? DateTime.UtcNow.AddMinutes(30) : DateTime.UtcNow.AddDays(7), 
                signingCredentials: creds
            );

            response.Id = id;
            response.Email = email;
            response.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return response;
        }

        public Task<string> GenerateRefreshTokenAsync()
        {
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            return Task.FromResult(refreshToken);
        }

        public async Task SaveRefreshTokenAsync(RefreshTokenDto dto)
        {
            await _AuthRepository.SaveRefreshTokenAsync(dto);
        }
    }
}
