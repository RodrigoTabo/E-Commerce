using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.MetodoEnvios;

namespace E_Commerce.Client.ApiRoutes
{
    public class MetodoEnvioApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<MetodoEnvioDTO>> GetAllAsync()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<MetodoEnvioDTO>>("api/metodoEnvio");
        }

    }
}
