using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName {  get; set; }=string.Empty;
        public string Address { get; set; } = default!;
        public string? Picture {  get; set; }
        public bool IsDeleted {  get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = default!;

    }
}
