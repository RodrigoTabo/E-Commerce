using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.ProductoVariantes;

namespace E_Commerce.Client.ApiRoutes
{
    public class ProductoVarianteApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;

        public async Task<List<ListProductoVariante>> GetProductoVarianteByIdProducto(int idProducto)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<List<ListProductoVariante>>($"api/productovariante/producto/{idProducto}");
        }

        public async Task<int> CreateAsync(CreateProductoVarianteDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<CreateProductoVarianteDTO, CreatedIdResponse>("api/productovariante", request);
            return created.Id;
        }

        public async Task<ProductoVarianteDTO> GetByIdAsync(int id)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            return await client.GetJsonOrThrowAsync<ProductoVarianteDTO>($"api/productovariante/{id}");
        }

        public async Task PutAsync(UpdateProductoVarianteDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            await client.PutJsonOrThrowAsync<UpdateProductoVarianteDTO>("api/productovariante", request);
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }

    }
}
