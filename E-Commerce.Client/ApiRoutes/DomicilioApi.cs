using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Domicilios;

namespace E_Commerce.Client.ApiRoutes
{
    public class DomicilioApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<DomicilioDTO>> GetAllAsync()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<DomicilioDTO>>("api/domicilio");
        }

    }
}
