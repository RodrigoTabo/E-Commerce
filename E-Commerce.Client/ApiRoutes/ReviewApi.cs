using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Reviews;
using System.Net.Http.Json;

namespace E_Commerce.Client.ApiRoutes
{
    public class ReviewApi(ApiHttpClientProvider apiHttpClientProvider)
    {
        private readonly ApiHttpClientProvider _apiHttpClientProvider = apiHttpClientProvider;


        public async Task<int> CreateAsync(CrearReviewDTO request)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();
            var created = await client.PostJsonOrThrowAsync<CrearReviewDTO, CreatedIdResponse>("api/review", request);
            return created.Id;
        }

        public async Task DeleteAsync(int reviewId, int productId)
        {
            var client = await _apiHttpClientProvider.GetClientAsync();

            var url = $"api/review/{reviewId}?productId={productId}";

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }

}
