using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Orden;
using E_Commerce.Shared.DTOs.OrdenCreate;
using Microsoft.AspNetCore.Components.Forms;

namespace E_Commerce.Client.ApiRoutes
{
    public class OrdenApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<OrdenDetailsUserDTO>> ListOrdenByUsers()
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<OrdenDetailsUserDTO>>("api/orden/mis-compras");
        }

        public async Task<int> CreateAsync(CrearOrdenConPagoRequest request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<CrearOrdenConPagoRequest, CreatedIdResponse>("api/orden", request);
            return created.Id;
        }

        public async Task CargarComprobante(int ordenId, IBrowserFile archivo)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(ordenId.ToString()), "OrdenId");

            var fileContent = new StreamContent(archivo.OpenReadStream(maxAllowedSize: 10_000_000));

            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(archivo.ContentType);

            content.Add(fileContent, "Archivo", archivo.Name);

            var response = await client.PostAsync("api/orden/cargar-comprobante", content);

            response.EnsureSuccessStatusCode();
        }


        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }
}
