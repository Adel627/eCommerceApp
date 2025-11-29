using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Helpers
{
    public interface IImageService
    {
        Task<(bool upload, string? errorMessage)> UploadImage(IFormFile image, string imageName, string folderPath);

         void Delete(string imagePath);

    }
}
