using System.Security.Claims;

namespace eCommerceApp.Host.Extensions
{
    public static class UserExtension
    {
        public static string? GetUserId(this ClaimsPrincipal User) =>

             User.FindFirstValue(ClaimTypes.NameIdentifier);
        
    }
}
