using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerceApp.Application.DTOs.Authentication
{
    public class CreateUser
    {
        [Required]
        public  string FullName { get; set; } =string.Empty;
        public string Email { get; set; } = string.Empty;
        public  string Password { get; set; } = string.Empty;
        public  string ConfirmPassword { get; set; } = string.Empty;
    }
}
