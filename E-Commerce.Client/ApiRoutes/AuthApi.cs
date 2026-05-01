using E_Commerce.Shared.DTOs.Auth;
using System.Net.Http.Json;

namespace E_Commerce.Client.ApiRoutes
{
    public class AuthApi(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result is null)
                throw new Exception("No se pudo leer la respuesta de login.");

            return result;
        }
        public async Task<LoginResponse> SelectBranchAsync(string sucursalId)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/select-branch", sucursalId);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (result is null) throw new Exception("Error al obtener el token de sucursal.");

            return result;
        }
    }
}