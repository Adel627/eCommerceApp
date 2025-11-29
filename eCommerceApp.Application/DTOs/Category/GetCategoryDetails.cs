using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Category
{
    public class GetCategoryDetails : GetCategory
    {
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedById { get; set; } = default!;
    }
}
