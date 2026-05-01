using System.Net.Http.Headers;

namespace E_Commerce.Client.Common
{
    public class ApiHttpClientProvider(HttpClient httpClient, TokenStorageService tokenStorage)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly TokenStorageService _tokenStorage = tokenStorage;

        public async Task<HttpClient> GetClientAsync()
        {
            var token = await _tokenStorage.GetTokenAsync();

            if (!string.IsNullOrWhiteSpace(token))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            else
                _httpClient.DefaultRequestHeaders.Authorization = null;

            return _httpClient;
        }

        public void ClearAuthorization()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}

