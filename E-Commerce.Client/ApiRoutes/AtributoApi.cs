using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Atributos;

namespace E_Commerce.Client.ApiRoutes
{
    public class AtributoApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<AtributosResponseDTO>> GetAllAsync()
        {

            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<AtributosResponseDTO>>("api/atributo");
        }

        public async Task<int> CreateAsync(CreateAtributoRequestDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<CreateAtributoRequestDTO, CreatedIdResponse>("api/atributo/crear", request);
            return created.Id;
        }

        public async Task PutAsync(UpdateAtributoRequestDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.PutJsonOrThrowAsync("api/atributo/update", request);
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }
}
