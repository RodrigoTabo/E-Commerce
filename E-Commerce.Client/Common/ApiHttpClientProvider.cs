namespace E_Commerce.Client.Common
{
    public class ApiHttpClientProvider(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public Task<HttpClient> GetClientAsync()
        {
            return Task.FromResult(_httpClient);
        }

        public void ClearAuthorization()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}

