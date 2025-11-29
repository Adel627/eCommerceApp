using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Helpers
{
    public class ImageService(IWebHostEnvironment webHostEnvironment) : IImageService
    {
        private readonly List<string> _allowedExtensions = new() { ".jpg", ".jpeg", ".png" };
        private readonly int _maxAllowedSize = 2097152;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<(bool upload , string? errorMessage)> UploadImage(IFormFile image, string imageName , string folderPath)
        {
           var extension = Path.GetExtension(image.FileName);
            if (!_allowedExtensions.Contains(extension))
                return (false, "Not allowed extension");

            if(image.Length > _maxAllowedSize)
                return (false, "Not allowed Size");
            var path = Path.Combine(_webHostEnvironment.WebRootPath , folderPath , imageName );

            using var stream = File.Create(path);
            await image.CopyToAsync(stream);
            stream.Dispose();

            return (true , null);
        }

        public void Delete(string imagePath)
        {
            var oldImagePath = Path.Combine (_webHostEnvironment.WebRootPath , imagePath);

            if (File.Exists(oldImagePath))
                File.Delete(oldImagePath);
        }
    }
}
