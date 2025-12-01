using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerceApp.Application.DTOs.Authentication
{
    public class CreateUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = default!;
        public IFormFile? Picture { get; set; }
        [Required]
        public  string Password { get; set; } = string.Empty;
        [Required]
        public  string ConfirmPassword { get; set; } = string.Empty;
    }
}
