using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Authentication
{
    public class CreateUser
    {
        public required string FullName { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public required string ConfirmPassword { get; set; } = string.Empty;
    }
}
