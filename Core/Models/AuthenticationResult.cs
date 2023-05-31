using System;

namespace Core.Models
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public string? Token { get; set; }
    }
}

