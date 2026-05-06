using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.User;
using System.Net.Http.Json;

namespace E_Commerce.Client.ApiRoutes
{
    public class UserApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;


        public async Task<UserRequestDTO> GetByUserId()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<UserRequestDTO>("api/perfil");
        }

        public async Task UpdateById(UserRequestDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.PutJsonOrThrowAsync("api/perfil", request);
        }

    }
}
