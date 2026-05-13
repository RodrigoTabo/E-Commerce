namespace E_Commerce.Shared.DTOs.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = default!;
        public string TokenType { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
