using Microsoft.AspNetCore.Hosting;
using Nia.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly string _imageDirectory;

        public ImageService(IWebHostEnvironment env)
        {
            _imageDirectory = Path.Combine(env.WebRootPath, "images");
        }

        public (byte[] FileBytes, string ContentType)? GetImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            var filePath = Path.Combine(_imageDirectory, fileName);

            if (!File.Exists(filePath))
                return null;

            var contentType = GetContentType(filePath);
            var fileBytes = File.ReadAllBytes(filePath);

            return (fileBytes, contentType);
        }

        private string GetContentType(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
        }
    }
}
