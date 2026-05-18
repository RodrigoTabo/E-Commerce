using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace E_Commerce.Shared.DTOs.User
{
    public class UserRegisterDTO
    {
        public string Email { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string DNI { get; set; } = null!;
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
