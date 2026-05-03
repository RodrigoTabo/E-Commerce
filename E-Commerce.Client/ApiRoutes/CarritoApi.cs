using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Carritos;

namespace E_Commerce.Client.ApiRoutes
{
    public class CarritoApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<CarritoDto?> CarritoUser()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<CarritoDto>("api/Carrito");
        }

        public async Task<int> CreateAsync(AgregarCarritoDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<AgregarCarritoDTO, CreatedIdResponse>("api/Carrito", request);
            return created.Id;
        }

        public async Task RestarCantidad(int IdProducto)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.PatchOrThrowAsync($"api/Carrito/restar/{IdProducto}");
        }

        public async Task SumarCantidad(int IdProducto)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.PatchOrThrowAsync($"api/Carrito/sumar/{IdProducto}");
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }
}
