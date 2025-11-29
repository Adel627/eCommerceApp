using eCommerceApp.Domain.Entities.Identity;
using System.Security.Claims;

namespace eCommerceApp.Domain.Interfaces.Authentication
{
    public interface IUserManagement
    {
        Task<bool> CreateUser(AppUser user);
        Task<bool> LoginUser(AppUser user);
        Task<AppUser?> GetUserByEmail(string email);
        Task<AppUser> GetUserById(string id);
        Task<bool> CheckeByUserName(string userName);
        Task<AppUser?> GetUserByEmailOrUserName(string emailORusername);

        Task<bool> CheckeByPhoneNumber(string phoneNumber);
        Task<IEnumerable<AppUser>?> GetAllUsers();
        Task<int> RemoveUserByEmail(string email);
        Task<List<Claim>> GetUserClaims(string email);
    }
}
