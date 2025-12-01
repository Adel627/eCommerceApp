using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class TokenManagement(AppDbContext context, IConfiguration config) : ITokenManagement
    {
        private readonly AppDbContext _context = context;
        private readonly IConfiguration _config = config;

        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            var RefreshToken = new RefreshToken()
            {
                UserId = userId,
                Token = refreshToken
            };
            await _context.AddAsync(RefreshToken);
            return await _context.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                expires: DateTime.UtcNow.AddHours(2),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                       );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomBytes = new byte[byteSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            string token = Convert.ToBase64String(randomBytes);

            return WebUtility.UrlEncode(token);
        }

        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken == null) return [];
            return jwtToken.Claims.ToList();
        }

        public async Task<string> GetUserIdByRefreshToken(string refreshToken) =>
         (await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken))!.UserId;


        public async Task<int> UpdateRefreshToken(string userId, string oldRefreshToken, string refreshToken)
        {
            var userToken =
                 await _context.RefreshTokens.FirstOrDefaultAsync(r => r.UserId == userId && r.Token == oldRefreshToken);
            if (userToken == null) return -1;

            userToken.Token = refreshToken;
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var userRefreshToken =
                await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token.Equals(refreshToken));

            return userRefreshToken != null;
            
        }
        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            RefreshToken? refreshTokenEntity = await _context.RefreshTokens
                .FirstOrDefaultAsync( r => r.Token == refreshToken);

             _context.RefreshTokens.Remove(refreshTokenEntity!);
            await _context.SaveChangesAsync();
        }
    }
}
