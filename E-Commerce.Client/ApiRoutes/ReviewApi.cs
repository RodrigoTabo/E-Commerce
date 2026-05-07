using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Reviews;

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

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }

}
