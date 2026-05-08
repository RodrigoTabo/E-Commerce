using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.AtributoValores;

namespace E_Commerce.Client.ApiRoutes
{
    public class AtributoValorApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<ListAtributoValorResponseDTO>> GetAllAsync()
        {

            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<ListAtributoValorResponseDTO>>("api/atributovalor");
        }

        public async Task<int> CreateAsync(CreateAtributoValorRequestDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<CreateAtributoValorRequestDTO, CreatedIdResponse>("api/atributovalor/crear", request);
            return created.Id;
        }

        public async Task PutAsync(UpdateAtributoValorRequestDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.PutJsonOrThrowAsync<UpdateAtributoValorRequestDTO>("api/atributovalor/update", request);
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }
}
