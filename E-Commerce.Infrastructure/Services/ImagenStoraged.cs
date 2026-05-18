using E_Commerce.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Services
{
    public class ImagenStorage : IImagenStorageService
    {
        private readonly IWebHostEnvironment _env;

        private static readonly HashSet<string> _allowed = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/png",
        "image/jpeg",
        "image/webp"
    };

        public ImagenStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveAsync(Stream stream, string contentType, string folder, CancellationToken ct = default)
        {
            if (stream is null)
                throw new InvalidOperationException("Archivo vacío.");

            if (!_allowed.Contains(contentType))
                throw new InvalidOperationException("Formato no permitido.");

            using var ms = new MemoryStream();

            await stream.CopyToAsync(ms, ct);

            if (ms.Length == 0)
                throw new InvalidOperationException("Archivo vacío.");

            if (ms.Length > 2 * 1024 * 1024)
                throw new InvalidOperationException("Supera el límite de 2 MB.");

            ms.Position = 0;

            using var image = await Image.LoadAsync(ms, ct);

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(512, 512),
                Mode = ResizeMode.Crop
            }));

            var ext = ".webp";

            var relativeFolder = Path.Combine("uploads", folder);

            var absoluteFolder = Path.Combine(
                _env.WebRootPath,
                relativeFolder);

            Directory.CreateDirectory(absoluteFolder);

            var fileName = $"{Guid.NewGuid():N}{ext}";

            var absolutePath = Path.Combine(
                absoluteFolder,
                fileName);

            await image.SaveAsWebpAsync(absolutePath, ct);

            var relativePath = Path.Combine(
                    "/",
                    relativeFolder,
                    fileName)
                .Replace("\\", "/");

            return relativePath;
        }

        public Task DeleteAsync(string? relativePath, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return Task.CompletedTask;

            var absolutePath = Path.Combine(
                _env.WebRootPath,
                relativePath
                    .TrimStart('/')
                    .Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(absolutePath))
                File.Delete(absolutePath);

            return Task.CompletedTask;
        }
    }
}
