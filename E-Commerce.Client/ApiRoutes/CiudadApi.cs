using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Ciudades;

namespace E_Commerce.Client.ApiRoutes
{
    public class CiudadApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<CiudadesDTO>> GetAllAsync()
        {

            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<CiudadesDTO>>("api/ciudad");
        }

    }
}
