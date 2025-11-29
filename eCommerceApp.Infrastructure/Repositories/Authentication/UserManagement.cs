using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class UserManagement(UserManager<AppUser> userManager , IRoleManagement roleManagement ,  AppDbContext context) : IUserManagement
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IRoleManagement _roleManagement = roleManagement;
        private readonly AppDbContext _context = context;


        public async Task<bool> CheckeByPhoneNumber(string phoneNumber) =>
        
            await _userManager.Users.AnyAsync(u => u.PhoneNumber == phoneNumber); 
        

        public async Task<bool> CheckeByUserName(string userName) =>

            await _userManager.Users.AnyAsync(u => u.UserName == userName);

        public async Task<AppUser?> GetUserByEmailOrUserName(string emailORusername)=>

             await _userManager.Users
                .SingleOrDefaultAsync(u => u.Email == emailORusername || u.UserName == emailORusername);



        public async Task<bool> CreateUser(AppUser user)
        {
            var userExsits = await GetUserByEmail(user.Email!);
            if(userExsits != null) return false;

           var result = await _userManager.CreateAsync(user , user.PasswordHash!);
            return result.Succeeded;
        }

        public async Task<IEnumerable<AppUser>?> GetAllUsers() =>
          
            await _context.Users.ToListAsync();
        
        public async Task<AppUser?> GetUserByEmail(string email) =>
           
            await _userManager.FindByEmailAsync(email);

        public async Task<AppUser> GetUserById(string id)
        {
          var user = await _userManager.FindByIdAsync(id);
            return user!;
        }

        public async Task<List<Claim>> GetUserClaims(string email)
        {
            var user = await GetUserByEmail(email);
            string? roleName = await _roleManagement.GetUserRole(email);
            List<Claim> claims = [
                new Claim("FullName" , user!.FullName) ,
                new Claim (ClaimTypes.NameIdentifier , user.Id) ,
                new Claim (ClaimTypes.Email , user.Email!) ,
                new Claim (JwtRegisteredClaimNames.PreferredUsername , user.UserName!) ,
                new Claim(ClaimTypes.Role, roleName!)
                ];
           return claims;
        }

        public async Task<bool> LoginUser(AppUser user)
        {
            var userExists = await GetUserByEmailOrUserName(user.Email!);
            
            if (userExists == null)  return false;

            string? roleName = await _roleManagement.GetUserRole(userExists.Email!);
             if(string.IsNullOrEmpty(roleName)) return false; 

            return await _userManager.CheckPasswordAsync(userExists  ,user.PasswordHash!);
        }

        public async Task<int> RemoveUserByEmail(string email)
        {
            var user = await GetUserByEmail(email);
            if (user == null) return 0;

               _context.Remove(user);
            return await _context.SaveChangesAsync();
        }
    }
}
