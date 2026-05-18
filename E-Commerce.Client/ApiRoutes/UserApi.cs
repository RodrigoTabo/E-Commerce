using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.User;
using System.Net.Http.Headers;

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

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(request.Nombre ?? string.Empty), nameof(request.Nombre));
            content.Add(new StringContent(request.Apellido ?? string.Empty), nameof(request.Apellido));
            content.Add(new StringContent(request.DNI ?? string.Empty), nameof(request.DNI));
            if (request.ImagenPerfil is not null)
            {
                using var stream = request.ImagenPerfil.OpenReadStream(2 * 1024 * 1024);

                var fileContent = new StreamContent(stream);

                fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.ImagenPerfil.ContentType);

                content.Add(fileContent, nameof(request.ImagenPerfilForm), request.ImagenPerfil.Name);

                var response = await client.PutAsync("api/perfil", content);
                response.EnsureSuccessStatusCode();
            }
            else
            {
                var response = await client.PutAsync("api/perfil", content);
                response.EnsureSuccessStatusCode();
            }
        }

    }
}
