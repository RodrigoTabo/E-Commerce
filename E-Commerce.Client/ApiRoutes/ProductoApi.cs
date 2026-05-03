using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Productos;
using System.Net.Http.Headers;

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

        public async Task<ProductoResponseDTO> GetByIdAsync(int id)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<ProductoResponseDTO>($"api/productos/{id}");
        }

        public async Task<int> CreateAsync(CreateProductoRequestDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created =  await client.PostJsonOrThrowAsync<CreateProductoRequestDTO, CreatedIdResponse>("api/productos", request);
            return created.Id;
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }

    }
}
