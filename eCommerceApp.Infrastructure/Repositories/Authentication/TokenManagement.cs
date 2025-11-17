using eCommerceApp.Domain.Interfaces.Authentication;
using System.Security.Claims;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class TokenManagement() : ITokenManagement
    {
        public Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public string GetRefreshToken()
        {
            throw new NotImplementedException();
        }

        public List<Claim> GetUserClaimsFromToken(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdByRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
