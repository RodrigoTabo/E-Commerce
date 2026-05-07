using E_Commerce.Client.Common;
using E_Commerce.Shared.DTOs.Auth;
using E_Commerce.Shared.DTOs.User;
using System.Net.Http.Json;

namespace E_Commerce.Client.ApiRoutes
{
    public class AuthApi(HttpClient httpClient, TokenStorageService tokenStorageService)
    {


        private readonly HttpClient _httpClient = httpClient;
        private readonly TokenStorageService _tokenStorageService = tokenStorageService;
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result is null)
                throw new Exception("No se pudo leer la respuesta de login.");

            await _tokenStorageService.SetTokenAsync(result.Token);

            return result;
        }

        public async Task<LoginResponse> RegisterAsync(UserRegisterDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result is null)
                throw new Exception("No se pudo leer la respuesta de login.");

            await _tokenStorageService.SetTokenAsync(result.Token);

            return result;
        }

    }
}