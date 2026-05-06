using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.OrdenCreate;

namespace E_Commerce.Client.ApiRoutes
{
    public class OrdenApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;
        public async Task<int> CreateAsync(CrearOrdenConPagoRequest request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<CrearOrdenConPagoRequest, CreatedIdResponse>("api/orden", request);
            return created.Id;
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }
}
