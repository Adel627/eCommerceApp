using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Entities
{
    public class BaseEntity
    {

        public DateTime CreatedDate { get; set; }= DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } 
        public string? CreatedById { get; set; } = default!;
        public AppUser CreatedBy { get; set; } = default!;
    }
}
