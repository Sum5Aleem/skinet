using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Helpers
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public int TokenExpiryInMinutes { get; set; }
        public int ResetTokenExpiryInMinutes { get; set; }
        public int RefreshTokenInMinutes { get; set; }
        public int IdleTimeOutInMinutes { get; set; }
    }
}
