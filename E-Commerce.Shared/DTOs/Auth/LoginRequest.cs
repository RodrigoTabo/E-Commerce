using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Auth
{
    public class LoginRequest
    {
        public string Identifier { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
