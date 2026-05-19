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

        public async Task<int> CreateAsync(CreateDomicilioDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<CreateDomicilioDTO, CreatedIdResponse>("api/domicilio", request);
            return created.Id;
        }

        public async Task PutAsync(UpdateDomicilioDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.PutJsonOrThrowAsync<UpdateDomicilioDTO>("api/domicilio", request);
        }

        public async Task DeleteAsync(int idDomicilio)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.DeleteOrThrowAsync($"api/domicilio/{idDomicilio}");
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }

    }
}
