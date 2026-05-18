using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Productos;
using System.Net.Http.Headers;
using static MudBlazor.CategoryTypes;

namespace E_Commerce.Client.ApiRoutes
{
    public class ProductoApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<ProductoResponseDTO>> GetAllAsync()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<ProductoResponseDTO>>("api/productos");
        }

        public async Task<ProductoDetalleDTO> GetByIdAsync(int id)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<ProductoDetalleDTO>($"api/productos/{id}");
        }

        public async Task<int> CreateAsync(CreateProductoRequestDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(request.Nombre ?? string.Empty), nameof(request.Nombre));
            content.Add(new StringContent(request.Descripcion ?? string.Empty), nameof(request.Descripcion));
            content.Add(new StringContent(request.IdModelo.ToString()), nameof(request.IdModelo));

            if (request.ImagenPerfil is not null)
            {
                var stream = request.ImagenPerfil.OpenReadStream(2 * 1024 * 1024);

                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.ImagenPerfil.ContentType);

                content.Add(fileContent, nameof(request.ImagenPerfilForm), request.ImagenPerfil.Name);
            }

            var response = await client.PostAsync("api/productos", content);
            response.EnsureSuccessStatusCode();

            var idString = await response.Content.ReadAsStringAsync();

            return int.Parse(idString);
        }

        public async Task PutAsync(UpdateProductoRequestDTO request)
        {

            var client = await _apiHttpClientProvider.GetClientAsync();
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(request.Nombre ?? string.Empty), nameof(request.Nombre));
            content.Add(new StringContent(request.Descripcion ?? string.Empty), nameof(request.Descripcion));
            content.Add(new StringContent(request.Id.ToString()), nameof(request.Id));
            content.Add(new StringContent(request.IdModelo.ToString()), nameof(request.IdModelo));

            if (request.ImagenPerfil is not null)
            {
                using var stream = request.ImagenPerfil.OpenReadStream(2 * 1024 * 1024);

                var fileContent = new StreamContent(stream);

                fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.ImagenPerfil.ContentType);

                content.Add(fileContent, nameof(request.ImagenPerfilForm), request.ImagenPerfil.Name);

                var response = await client.PutAsync("api/productos/update", content);
                response.EnsureSuccessStatusCode();
            }
            else
            {
                var response = await client.PutAsync("api/productos/update", content);
                response.EnsureSuccessStatusCode();
            }
        }

    }
}
