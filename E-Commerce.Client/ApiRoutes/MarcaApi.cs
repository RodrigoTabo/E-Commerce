using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Marcas;

namespace E_Commerce.Client.ApiRoutes
{
    public class MarcaApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<MarcaDTO>> GetAllAsync()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<MarcaDTO>>("api/marca");
        }

        public async Task<int> CreateAsync(CreateMarcaDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<CreateMarcaDTO, CreatedIdResponse>("api/marca", request);
            return created.Id;
        }

        public async Task PutAsync(MarcaDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.PutJsonOrThrowAsync<MarcaDTO>("api/marca", request);
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }
}
