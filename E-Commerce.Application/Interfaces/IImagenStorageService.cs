using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces
{
    public interface IImagenStorageService
    {
        Task<string> SaveAsync(Stream stream, string contentType, string folder, CancellationToken ct = default);
        Task DeleteAsync(string? relativePath, CancellationToken ct = default);
    }
}
