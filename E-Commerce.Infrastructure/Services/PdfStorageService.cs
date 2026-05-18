using E_Commerce.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Services
{
    public class PdfStorageService : IPdfStorageService
    {
        private readonly IWebHostEnvironment _env;

        private static readonly HashSet<string> _allowed = new(StringComparer.OrdinalIgnoreCase)
        {
                "application/pdf"
        };

        public PdfStorageService(IWebHostEnvironment env)
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

            if (ms.Length > 5 * 1024 * 1024)
                throw new InvalidOperationException("Supera el límite de 5 MB.");

            ms.Position = 0;

            byte[] buffer = new byte[4];

            await ms.ReadAsync(buffer, ct);
            var header = Encoding.ASCII.GetString(buffer);

            if (header != "%PDF")
                throw new InvalidOperationException("PDF inválido.");
            ms.Position = 0;

            var ext = ".pdf";

            var relativeFolder = Path.Combine("uploads", folder);

            var absoluteFolder = Path.Combine(_env.WebRootPath, relativeFolder);

            Directory.CreateDirectory(absoluteFolder);

            var fileName = $"{Guid.NewGuid():N}{ext}";

            var absolutePath = Path.Combine(absoluteFolder, fileName);

            using var fileStream = new FileStream(absolutePath, FileMode.Create);

            await ms.CopyToAsync(fileStream, ct);

            var relativePath = Path.Combine("/", relativeFolder, fileName).Replace("\\", "/");

            return relativePath;
        }
    }
}