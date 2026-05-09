using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Modelos;

namespace E_Commerce.Client.ApiRoutes
{
    public class ModeloApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<ModeloDetalleDTO>> GetAllAsync()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<ModeloDetalleDTO>>("api/modelo");
        }

        public async Task<List<ModeloResponseDTO>> GetAsync()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<ModeloResponseDTO>>("api/modelo/list");
        }

        public async Task<int> PostAsync(CreateModeloDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<CreateModeloDTO, CreatedIdResponse>("api/modelo", request);
            return created.Id;
        }


        public async Task PutAsync(ModeloResponseDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.PutJsonOrThrowAsync<ModeloResponseDTO>("api/modelo", request);
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }

    }
}
