namespace eCommerceApp.Application.DTOs.Authentication
{
    public record LoginResponse(
        bool Success = false,
        string Message = null! ,
         string Token = null!,
         string RefreshToken = null!
    );
}
