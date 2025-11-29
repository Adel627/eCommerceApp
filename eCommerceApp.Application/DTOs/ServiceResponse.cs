using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs
{
    public record ServiceResponse(bool Success = false , string Message = null!);
    public record ServiceResponse<T>(bool Success = false , string Message = null! ,
        dynamic Value = null!);
    
}
