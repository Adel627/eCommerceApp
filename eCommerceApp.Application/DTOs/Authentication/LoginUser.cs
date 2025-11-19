namespace eCommerceApp.Application.DTOs.Authentication
{
    public class LoginUser
    {
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}
