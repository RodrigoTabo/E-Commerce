using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.TipoProducto;

namespace E_Commerce.Client.ApiRoutes
{
    public class TipoProductoApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<TipoProductoDTO>> GetAsync()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<TipoProductoDTO>>("api/tipoproducto");
        }

        public async Task<int> Postasync(CreateTipoProductoDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<CreateTipoProductoDTO, CreatedIdResponse>("api/tipoproducto", request);
            return created.Id;
        }


        public async Task PutAsync(TipoProductoDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.PutJsonOrThrowAsync<TipoProductoDTO>("api/tipoproducto", request);
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }
}
