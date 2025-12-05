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
        public Cart Cart { get; set; } = default!;
        public ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
        public ICollection<Rates> Rates { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = default!;
        public ICollection<Order> Orders { get; set; } = default!;

    }
}
