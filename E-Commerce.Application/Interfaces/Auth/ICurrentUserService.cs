using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Auth
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }
        bool IsAdmin { get; }
    }
}
