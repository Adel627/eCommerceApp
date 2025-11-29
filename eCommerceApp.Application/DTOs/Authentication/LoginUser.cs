using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Authentication
{
    public class LoginUser
    {
        [Required]
        [Display(Name ="Email / UserNme")]
        public  string EmailorUserName { get; set; } = string.Empty;
        [Required]
        public  string Password { get; set; } = string.Empty;
    }
}
