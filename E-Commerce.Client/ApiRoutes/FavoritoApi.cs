using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Favoritos;

namespace E_Commerce.Client.ApiRoutes
{
    public class FavoritoApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;


        public async Task<List<FavoritoResponseDTO>> GetAGetFavoritosByUserllAsync()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<FavoritoResponseDTO>>("api/favorito");
        }

        public async Task ToggleFavorito(int idProducto)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var response = await client.PostAsync($"api/Favorito/{idProducto}", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
